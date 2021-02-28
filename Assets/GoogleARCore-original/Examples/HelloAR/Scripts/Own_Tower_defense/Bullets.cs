using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public int damage = 50;
    // 0이면 터렛, 0보다 크면 미사일
    public float explosionRadius = 0f;
    public GameObject ImpactEffect;

    private void Start()
    {
        // 범위나 스피드 같은 건 전부 스케일해줘야 한다..
        speed *= GameManager.Instance.map_scaled_factor;
        explosionRadius *= GameManager.Instance.map_scaled_factor;
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        // 혹시모를 불상사 대비
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        // 한 프레임당 총알이 움직이는 거리
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            // 이미 총알이 목표물에 도달
            HitTarget(target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        // 날아가는 도중에도 타겟을 바라보게끔.
        transform.LookAt(target);
    }
    
    void HitTarget(Transform target)
    {
        GameObject effectIns = Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if(explosionRadius > 0f)
        {
            // 미사일 -> 여러명 폭파
            Explode();
        }
        else
        {
            // 터렛 -> 한명만 제거
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        // 여기서 enemy 태그를 가진 애들만 고른다.
        foreach (Collider collider in colliders)
        {
            // collider의 tag도 되네?
            if (collider.tag == "Enemy")
                Damage(collider.transform);
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        // 불상사 대비
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}
