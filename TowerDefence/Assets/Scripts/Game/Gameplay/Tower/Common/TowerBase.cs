using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ëþ»ù
public class TowerBase : MonoBehaviour, IClickable
{
    private bool _isBuild = false;
    private Tower _tower;

    public bool IsBuild { get { return _isBuild; } set { _isBuild = value; } }

    public void OnClick()
    {
        if (!_isBuild)
        {
            ClickEvents.ClickTowerBase?.Invoke(this);
        }
    }

    public void ResetSelf()
    {
        _isBuild = false;
    }
}
