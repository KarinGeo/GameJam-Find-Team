using Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    private SpriteRenderer _sprit;
    private TowerClick _click;

    protected int _level;
    protected Vector3 _firePoint;
    protected TowerSO _towerData;
    protected List<Enemy> _enemys;
    protected TowerRange _range;
    protected ConfigData _config;

    public List<Enemy> Enemys { get { return _enemys; } set { _enemys = value; } }

    public virtual void Awake()
    {
        _enemys = new List<Enemy>();
        _firePoint = transform.Find("FirePoint").position;
        _config = ConfigManager.Instance.GetConfigData("TowerData");

        _level = 1;

        _sprit = transform.Find("Click").GetComponent<SpriteRenderer>();
        
        _click = transform.Find("Click").AddComponent<TowerClick>();
        _click.Init(this);
       
        _range = transform.Find("Range").AddComponent<TowerRange>();
        _range.Init(this);
    }

    public virtual void OnClick()
    {
        ClickEvents.ClickTower?.Invoke(this);
    }

    public virtual void Init(TowerSO data)
    {
        _towerData = data;
    }

    public virtual void Update()
    {

    }

    public virtual void Upgrade()
    {
        if (_level < _towerData.Level.Count)
        {
            _level += 1;
            _sprit.sprite = _towerData.Level[_level - 1];
        }
    }

    public virtual void Destroy()
    {
        transform.parent.GetComponent<TowerBase>().ResetSelf();
        Destroy(gameObject);
    }

    public virtual int GetCost()
    {
        if (_level < _towerData.Cost.Count)
        {
            return _towerData.Cost[_level];
        }
        else
        {
            return Utils.MaxInt;
        }
    }

    public virtual string GetCostShow()
    {
        if (_level < _towerData.Cost.Count)
        {
            return _towerData.Cost[_level].ToString();
        }
        else
        {
            return "Max";
        }
    }

    public virtual int GetSale()
    {
        return _towerData.Sale[_level - 1];
    }

    public virtual string GetSaleShow()
    {
        return GetSale().ToString();
    }
}
