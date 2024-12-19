using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Framework;
using System.Linq;
public class BuildView : BaseView
{
    AllTowerSO _towers;

    BuildController _buildController;

    public override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        
    }

    public override void Init(params object[] args)
    {
        base.Init(args);
        _buildController = Controller as BuildController;
        _towers = ResourceManager.Instance.GetDataCache<AllTowerSO>("TowerData/AllTowers.asset");

        GameObject towerPrefabObj = Find("Grid/Tower");
        Transform gridTf = Find("Grid").transform;

        for (int i = 0; i < _towers.Towers.Count; i++)
        {
            GameObject obj = Object.Instantiate(towerPrefabObj, gridTf);
            obj.GetComponent<Image>().sprite = _towers.Towers[i].Level.Last();
            obj.transform.Find("Cost").GetComponent<Text>().text = _towers.Towers[i].Cost.First().ToString();
            Button btn = obj.GetComponent<Button>();
            int currentIndex = i;   //必须要这么一个中间变量
            btn.onClick.AddListener(() => OnBuildButtonClick(currentIndex));

            obj.SetActive(true);
        }
    }

    public void OnBuildButtonClick(int i)
    {
        _buildController.TowerData = _towers.Towers[i];
        if (_towers.Towers[i].Cost[0] <= GameplayManager.Instance.Gold)
        {
            GameEvents.BuildTower?.Invoke();
            GameEvents.GoldDown?.Invoke(_towers.Towers[i].Cost[0]);
        }
    }

    public override void Show(params object[] args)
    {
        base.Show(args);
    }
}
