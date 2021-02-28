using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI roundsText;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        // retry 버튼을 눌렀을 때 씬 재시작
        // 그럼 평면도 다시 배치를 해야하겠지..? 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
