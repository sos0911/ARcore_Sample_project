using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeUI : MonoBehaviour
{
    public TextMeshProUGUI livestext;

    private void Update()
    {
        livestext.text = "Life : " + PlayerStats.Lives.ToString();
    }
}
