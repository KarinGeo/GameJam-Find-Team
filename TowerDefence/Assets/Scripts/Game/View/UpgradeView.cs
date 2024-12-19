using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Framework;
public class UpgradeView : BaseView
{
    Text _upgardCost;
    Text _destoryCost;

    public override void Awake()
    {
        base.Awake();

    }

    public override void Init(params object[] args)
    {
        base.Init(args);
        Button upgradBtn = transform.Find("Grid/Upgrade").GetComponent<Button>();
        upgradBtn.onClick.AddListener(() => GameEvents.UpgradeTower?.Invoke());

        Button destroyBtn = transform.Find("Grid/Destory").GetComponent<Button>();
        destroyBtn.onClick.AddListener(() => GameEvents.DestroyTower?.Invoke());
    }

    public override void Show(params object[] args)
    {
        base.Show(args);
        transform.Find("Grid/Upgrade/Cost").GetComponent<Text>().text = args[0] as string;
        transform.Find("Grid/Destory/Sale").GetComponent<Text>().text = args[1] as string;
    }

    


}
