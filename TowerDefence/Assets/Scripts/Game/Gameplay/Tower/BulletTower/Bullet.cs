using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Framework;

public class Bullet : DamageObject
{
    private float _speed = 300f;
    private float _damage = 0;
    private Vector3 _lastTargetPosition;

    public override void Init(Enemy target, float damage)
    {
        base.Init(target, damage);
        _damage = damage;
        _lastTargetPosition = target.transform.position;
    }

    public override void Update()
    {
        if (_target != null && _target.IsDie)
        {
            _target = null;
        }

        Vector2 direction;

        if (_target != null)
        {
            direction = Utils.GetDistanceNormalized2D(transform.position, _target.transform.position);
            _lastTargetPosition = _target.transform.position;
        }
        else
        {
            direction = Utils.GetDistanceNormalized2D(transform.position, _lastTargetPosition);

            if (Utils.GetDistance2D(transform.position, _lastTargetPosition) <= 0.1)
            {
                Destroy(gameObject);
            }
        }

        transform.position += new Vector3(direction.x, direction.y, 0) * _speed * Time.deltaTime;
        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0, 0, rotationZ * Mathf.Rad2Deg);


    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_target != null && collision.GetComponent<Enemy>() == _target)
        {
            if (_target != null)
            {
                _target.GetHurt(_damage);
            }
            Destroy(gameObject);
        }
    }
}
