using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneEvents
{
    public static Action<string> LoadSceneByName;
    public static Action<int> LoadSceneByLevel;


    public static Action ExitApplication;
}
