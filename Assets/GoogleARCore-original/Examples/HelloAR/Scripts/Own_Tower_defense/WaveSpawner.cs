using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    // 게임이 모두 끝나 승리?
    public static bool Victory = false;


    // 각 wave당 속성
    public GameObject[] WaveEnemyPrefabs;
    public int[] waveEnemycount;
    public float[] waveEnemyrate;

    public Transform spawnPoint;

    // 현재 살아있는 적 수
    public static int EnemiesAlive = 0;

    // 웨이브 사이 간격
    public float timeBetweenWaves = 5f;
    // 다음 웨이브까지의 남은 시간
    private float countdown = 2f;

    private int waveIndex = 0;

    private TextMeshProUGUI wavecountdownText;

    private void Start()
    {
        // 찾아야 한다.. 씬에서
        wavecountdownText = GameManager.Instance.UI_text_cache;
    }

    private void Update()
    {
        if (waveIndex == WaveEnemyPrefabs.Length && EnemiesAlive == 0)
        {
            // 게임 승리!
            Victory = true;
            // 적을 더 이상 생성x
            this.enabled = false;
            return;
        }

        // 적이 하나라도 살아있다면 다음 웨이브(라운드)를 시작x
        if (EnemiesAlive > 0)
            return;

        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        wavecountdownText.text = "Time to next Wave : " + Mathf.Round(countdown).ToString();
    }

    /// <summary>
    /// 웨이브 시작
    /// </summary>
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;


        for (int i = 0; i < waveEnemycount[waveIndex]; i++)
        {
            SpawnEnemy(WaveEnemyPrefabs[waveIndex]);
            yield return new WaitForSeconds(1f/waveEnemyrate[waveIndex]); 
        }

        waveIndex++;
    }

    /// <summary>
    /// 하나의 적 스폰
    /// </summary>
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
