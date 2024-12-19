using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : BaseController
{
    public UIController() : base()
    {
        RegisterViews();
        SubscribeToEvents();
    }

    public override void RegisterViews()
    {
        base.RegisterViews();
        ViewManager.Instance.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            Controller = this,
            ParentTf = ViewManager.Instance.CanvasTf,
        });

        ViewManager.Instance.Register(ViewType.SelectLevelView, new ViewInfo()
        {
            PrefabName = "SelectLevelView",
            Controller = this,
            ParentTf = ViewManager.Instance.CanvasTf,
        });

        ViewManager.Instance.Register(ViewType.GameWinView, new ViewInfo()
        {
            PrefabName = "GameWinView",
            Controller = this,
            ParentTf = ViewManager.Instance.CanvasTf,
        });

        ViewManager.Instance.Register(ViewType.GameLossView, new ViewInfo()
        {
            PrefabName = "GameLossView",
            Controller = this,
            ParentTf = ViewManager.Instance.CanvasTf,
        });

    }

    public override void SubscribeToEvents()
    {
        UIEvents.StartViewOpen += UIEvents_StartViewOpen;
        UIEvents.SelectLevelViewOpen += UIEvents_SelectLevelViewOpen;
        UIEvents.GameWinViewOpen += UIEvents_GameWinViewOpen;
        UIEvents.GameLossViewOpen += UIEvents_GameLossViewOpen;

    }

    public void UIEvents_StartViewOpen()
    {
        ViewManager.Instance.Open(ViewType.StartView);
    }

    public void UIEvents_SelectLevelViewOpen()
    {
        ViewManager.Instance.Open(ViewType.SelectLevelView);
    }

    public void UIEvents_GameWinViewOpen()
    {
        int levelId = GameplayManager.Instance.LevelId;
        int health = GameplayManager.Instance.Health;

        

        ViewManager.Instance.Open(ViewType.GameWinView, levelId, health);
    }

    public void UIEvents_GameLossViewOpen()
    {
        ViewManager.Instance.Open(ViewType.GameLossView);
    }

}





