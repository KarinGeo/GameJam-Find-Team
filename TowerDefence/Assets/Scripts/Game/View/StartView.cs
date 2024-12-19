using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartView : BaseView
{
    public override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        Find<Button>("StartBtn").onClick.AddListener(() => UIEvents.SelectLevelViewOpen?.Invoke());
        Find<Button>("SetBtn").onClick.AddListener(() => UIEvents.MainSettingViewOpen?.Invoke());
        Find<Button>("ExitBtn").onClick.AddListener(() => SceneEvents.ExitApplication?.Invoke());
    }
}
