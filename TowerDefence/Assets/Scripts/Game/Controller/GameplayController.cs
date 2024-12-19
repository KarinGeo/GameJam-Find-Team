using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : BaseController
{
    public GameplayController() : base()
    {
        RegisterViews();
        SubscribeToEvents();
    }

    public override void RegisterViews()
    {
        base.RegisterViews();
        ViewManager.Instance.Register(ViewType.GameplayView, new ViewInfo()
        {
            PrefabName = "GameplayView",
            Controller = this,
            ParentTf = ViewManager.Instance.CanvasTf,
        });
    }

    public override void SubscribeToEvents()
    {
        UIEvents.GameplayViewOpen += UIEvents_GameplayViewOpen;
    }

    public void UIEvents_GameplayViewOpen()
    {
        ViewManager.Instance.Open(ViewType.GameplayView);
    }
}
