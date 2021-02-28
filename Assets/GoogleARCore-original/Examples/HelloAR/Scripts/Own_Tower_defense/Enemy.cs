using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public float maxhealth = 100;
    // 현재 체력
    private float currenthealth;

    // 죽일 때 얻는 돈
    public int gainmoney = 50;
    // 죽을 때 쓰일 파티클 효과
    public GameObject deathEffect;

    private Transform target;
    private int wavepointindex = 0; // 0번째 waypoint부터 시작
    public float scale_factor;// 실제 시연에서 얼마나 맵을 줄였나?

    [Header("unity stuff")]
    public GameObject health_canvas;
    private Camera FPS; // AR 카메라
    public Image healthbar;

    private void Start()
    {
        target = WayPoints.points[0];
        scale_factor = GameManager.Instance.map_scaled_factor;
        FPS = GameManager.Instance.FPS;
        currenthealth = maxhealth;
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * scale_factor * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.2f* scale_factor)
        {
            GetNextWayPoint();
        }

        // 또한 AR 씬에서 사용자를 항상 헬스바가 보고 있게끔 한다.
        health_canvas.transform.LookAt(FPS.transform);
    }

    public void TakeDamage(int amount)
    {
        // 피격 함수.
        currenthealth -= amount;
        healthbar.fillAmount = currenthealth / maxhealth;

        if (currenthealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += gainmoney;
        Debug.Log("Money : " + PlayerStats.Money);
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);

        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
        Destroy(effect, 2f);
    }

    void GetNextWayPoint()
    {
        if(wavepointindex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointindex++;
        target = WayPoints.points[wavepointindex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
