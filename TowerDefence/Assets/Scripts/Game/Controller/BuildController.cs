using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
using static UnityEditor.PlayerSettings;

public class BuildController : BaseController
{
    private TowerSO _towerData;
    private TowerBase _towerBase;
    private Tower _tower;

    public TowerSO TowerData { get { return _towerData; } set { _towerData = value; } }

    public BuildController() : base()
    {
        RegisterViews();
        SubscribeToEvents();
    }

    public override void RegisterViews()
    {
        base.RegisterViews();
        ViewManager.Instance.Register(ViewType.BuildView, new ViewInfo()
        {
            PrefabName = "BuildView",
            Controller = this,
            ParentTf = ViewManager.Instance.WorldCanvasTf,
        });

        base.RegisterViews();
        ViewManager.Instance.Register(ViewType.UpgradeView, new ViewInfo()
        {
            PrefabName = "UpgradeView",
            Controller = this,
            ParentTf = ViewManager.Instance.WorldCanvasTf,
        });
    }

    public override void SubscribeToEvents()
    {
        UIEvents.BuildViewShow += UIEvents_BuildViewShow;
        UIEvents.BuildViewHide += UIEvents_BuildViewHide;

        UIEvents.UpgradeViewShow += UIEvents_UpgradeViewShow;
        UIEvents.UpgradeViewHide += UIEvents_UpgradeViewHide;

        ClickEvents.ClickTowerBase += ClickEvents_ClickTowerBase;
        ClickEvents.UnClickTowerBase += ClickEvents_UnClickTowerBase;

        ClickEvents.ClickTower += ClickEvents_ClickTower;
        ClickEvents.UnClickTower += ClickEvents_UnClickTower;

        GameEvents.BuildTower += GameEvents_BuildTower;
        GameEvents.UpgradeTower += GameEvents_UpgradeTower;
        GameEvents.DestroyTower += GameEvents_DestroyTower;
    }

    public void GameEvents_BuildTower()
    {
        if (!_towerBase.IsBuild)
        {
            Vector3 buildPosition = _towerBase.transform.position - new Vector3(0, 0, 1);
            GameObject obj = GameObject.Instantiate(_towerData.Prefab, buildPosition, Quaternion.identity, _towerBase.transform);


            _towerBase.IsBuild = true;
            ClickEvents.UnClickTowerBase?.Invoke();
            switch (_towerData.TowerType)
            {
                case TowerType.BulletTower:
                    obj.AddComponent<BulletTower>().Init(_towerData);
                    return;
                case TowerType.LaserTower:
                    obj.AddComponent<LaserTower>().Init(_towerData);
                    return;
                default:
                    return;
            }
        }
    }

    public void GameEvents_UpgradeTower()
    {
        int cost = _tower.GetCost();
        if (cost <= GameplayManager.Instance.Gold)
        {
            _tower.Upgrade();
            GameEvents.GoldDown?.Invoke(cost);
            ClickEvents.UnClickTower?.Invoke();
        }
    }

    public void GameEvents_DestroyTower()
    {
        int sale = _tower.GetSale();
        _tower.Destroy();
        GameEvents.GoldUp?.Invoke(sale);
        ClickEvents.UnClickTower?.Invoke();
    }

    public void ClickEvents_ClickTowerBase(TowerBase towerBase)
    {
        _towerBase = towerBase;
        UIEvents.BuildViewShow?.Invoke();
    }

    public void ClickEvents_UnClickTowerBase()
    {
        _towerBase = null;
        UIEvents.BuildViewHide?.Invoke();
    }

    public void ClickEvents_ClickTower(Tower tower)
    {
        _tower = tower;
        UIEvents.UpgradeViewShow?.Invoke();
        ViewManager.Instance.SetPosition(ViewType.UpgradeView, _tower.transform.position);
    }

    public void ClickEvents_UnClickTower()
    {
        _tower = null;
        UIEvents.UpgradeViewHide?.Invoke();
    }


    public void UIEvents_BuildViewShow()
    {
        TowerBase towerBase = _towerBase;
        ViewManager.Instance.Show(ViewType.BuildView, towerBase.transform.position, "Show");
    }

    public void UIEvents_BuildViewHide()
    {
        ViewManager.Instance.Hide(ViewType.BuildView);
    }

    public void UIEvents_UpgradeViewShow()
    {
        Tower tower = _tower;
        string cost = _tower.GetCostShow();
        string sale = _tower.GetSaleShow();

        ViewManager.Instance.Show(ViewType.UpgradeView, tower.transform.position, "Show", cost, sale);
    }

    public void UIEvents_UpgradeViewHide()
    {
        ViewManager.Instance.Hide(ViewType.UpgradeView);
    }


}
