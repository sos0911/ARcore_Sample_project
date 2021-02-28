using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneytext;

    private void Update()
    {
        moneytext.text = "$" + PlayerStats.Money.ToString();
    }
}
