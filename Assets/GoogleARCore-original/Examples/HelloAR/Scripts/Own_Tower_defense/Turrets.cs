using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [Header("Unity Setup Field")]
    public Transform target;
    // 터렛 반응 범위
    public string enemyTag = "Enemy";
    // 돌리는 피봇 기준점
    public Transform PartToRotate;
    public float turnSpeed = 10.0f;
    // 총알 프리팹
    public GameObject bulletPrefab;
    // 총알이 날아가기 시작하는 지점
    public Transform firePoint;

    [Header("Attribute")]
    // 분당 총알 쏘는 속도
    public float range = 15f;
    public float fireRate = 1f;
    // 무기 재사용 쿨타임
    private float fireCountdown = 0f;
     
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        range *= GameManager.Instance.map_scaled_factor;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }
    
    void Shoot()
    {
        GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullets bullet = bulletGo.GetComponent<Bullets>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }
}
