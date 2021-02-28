using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // 터렛 구매를 총괄
    public TurretBluePrint StandardTurret;
    public TurretBluePrint missileLancher;

    public void SelectStandardTurret()
    {
        GameManager.Instance.SelectTurretToBuild(0);
    }   
     
    public void SelectMissileLauncher()
    {
        GameManager.Instance.SelectTurretToBuild(1);
    }

}
