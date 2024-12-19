using Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class BulletTower : Tower
{
    private float _timer;
    private float _frequency;
    private float _damage;
    
    public override void Awake()
    {
        base.Awake();
        
        _timer = 0;
        _damage = 0;
        _frequency = 1;
    }

    public override void Init(TowerSO data)
    { 
        base.Init(data);
        Refresh();
        _timer = _frequency;
    }

    public void Refresh()
    {
        int towerDataId = int.Parse(_towerData.Id.ToString() + _level.ToString());
        _frequency = float.Parse(_config.GetDataById(towerDataId)["Frequency"]);
        _damage = float.Parse(_config.GetDataById(towerDataId)["Damage"]);
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if ((_timer >= _frequency) && (_enemys.Count > 0))
        {
            Vector3 firePoint = _firePoint - new Vector3(0, 0, 1);
            GameObject flyObj = Instantiate(_towerData.Obj, firePoint, Quaternion.identity);

            Enemy enemy = _enemys[0] as Enemy;
            flyObj.AddComponent<Bullet>().Init(enemy, _damage);

            _timer = 0;
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        Refresh();
    }
}
