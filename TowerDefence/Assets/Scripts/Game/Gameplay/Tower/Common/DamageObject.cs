using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class DamageObject : MonoBehaviour
{
    protected Enemy _target;


    public virtual void Init()
    {
        
    }

    public virtual void Init(Enemy target)
    {
        SetTarget(target);
    }


    public virtual void Init(Enemy target, float damage)
    {
        Init(target);
    }

    public virtual void SetTarget(Enemy target)
    {
        _target = target;
    }

    public virtual void Update()
    {
        
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }


}
