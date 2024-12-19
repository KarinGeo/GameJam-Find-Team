using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action StartGame;
    public static Action LoadSceneInfo;

    public static Action BuildTower;
    public static Action UpgradeTower;
    public static Action DestroyTower;

    public static Action HealthDown;
    public static Action<int> GoldDown;
    public static Action<int> GoldUp;

    public static Action StartSpawnEnemy;
    public static Action ResetManager;
    public static Action GameWin;
    public static Action GameLoss;
}
