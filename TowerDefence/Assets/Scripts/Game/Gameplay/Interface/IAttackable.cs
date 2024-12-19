using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//可以被攻击的接口，代替标签的使用
public interface IAttackable
{
    void GetHurt(float damage);
}
