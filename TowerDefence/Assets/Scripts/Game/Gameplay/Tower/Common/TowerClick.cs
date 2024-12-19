using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClick : Child<Tower>, IClickable
{
    public override void Init(Tower parent)
    {
        base.Init(parent);
    }

    public void OnClick()
    {
        Parent.OnClick();
    }


}
