using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClickEvents
{
    public static Action<TowerBase> ClickTowerBase;
    public static Action UnClickTowerBase;
    public static bool ClickTowerBaseFlag;

    public static Action<Tower> ClickTower;
    public static Action UnClickTower;
    public static bool ClickTowerFlag;
}
