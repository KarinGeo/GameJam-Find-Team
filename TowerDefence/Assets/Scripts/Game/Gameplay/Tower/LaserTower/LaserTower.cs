using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LaserTower : Tower
{
    private float _timer;
    private float _damage;
    private float _frequency;

    private Laser _laser;

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

        GameObject _lazerObj = Instantiate(_towerData.Obj, transform);
        _laser = _lazerObj.AddComponent<Laser>();
        _laser.Init(_firePoint);
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
        if (_enemys.Count > 0)
        {
            _timer += Time.deltaTime;
            if (_timer >= _frequency)
            {
                Enemy enemy = _enemys[0] as Enemy;
                enemy.GetHurt(_damage);
                _timer = 0;
            }

            _laser.SetTarget(_enemys[0]);
            _laser.OnActivate();
            if (!_laser.IsActive)
            {
                _laser.ActivateLaser();
            }
        }
        else
        {
            if (_laser.IsActive)
            {
                _laser.DeactivateLaser();
            }
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        Refresh();
    }
}
