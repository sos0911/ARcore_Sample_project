using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // 플레이어 상태 관리
    // 현재 돈은 어디에서나 접근 가능
    public static int Money = 0;
    public int startMoney = 400;

    public static int Lives = 0;
    public int startLives = 3;

    public static int Rounds;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
    }
}
