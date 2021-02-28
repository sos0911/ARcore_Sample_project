using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    // 속성값
    // 실제 공간에 맵이 배치되었는가?
    // 되었다면 이제 맵 배치는 종료.
    public bool Is_map_placed = false;
    // 실제 공간에서 map이 얼마나 작아졌는가?
    public float map_scaled_factor = 0.01f;

    public TextMeshProUGUI UI_text_cache;
    // FPS 카메라 캐치(enemy에서 쓰임)
    public Camera FPS;

    // 게임이 끝났는가?
    public static bool gameEnded = false;
    // 게임오버 UI
    public GameObject gameOverUI;

    // 터렛 프리팹들
    // 터렛 , 미사일 터렛
    public GameObject[] TurretPrefabs;
    // 터렛들 비용 array
    public int[] TurretCosts = { 100, 250 };
    // 지금 지을 터렛 프리팹 idx
    public int turretToBuildidx = -1;

    // 토글할 UI
    public GameObject TotalUI;

    public void SelectTurretToBuild(int turretidx)
    {
        Debug.Log("turret prefab is set!\n");
        turretToBuildidx = turretidx;
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
         gameEnded = false;
         Is_map_placed = false;

    }

    private void Update()
    {
        if (gameEnded)
            return;

        if(PlayerStats.Lives <= 0)
        {
            EndGame(false);
        }
        if (WaveSpawner.Victory)
        {
            EndGame(true);
        }
    }

    void EndGame(bool victory)
    {
        if (victory)
        {
            // 승리 문구로 UI 변경
            gameOverUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Victory!";
        }
        // TODO : 사망 파티클 인스턴스, 적 파괴
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void ToggleUI()
    {
        // caching한 UI를 토글
        if (TotalUI.activeSelf)
            TotalUI.SetActive(false);
        else
            TotalUI.SetActive(true);
       
    }
}
      