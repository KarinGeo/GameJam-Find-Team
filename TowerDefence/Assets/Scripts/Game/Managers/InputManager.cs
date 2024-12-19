using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : UnitySingleton<InputManager>
{

    //
    public override void Awake()
    {
        base.Awake();
    }

    public void OnEnable()
    {
        ClickEvents.ClickTowerBase += ClickEvents_ClickTowerBase;
        ClickEvents.ClickTower += ClickEvents_ClickTower;
    }

    public void OnDisable()
    {
        ClickEvents.ClickTowerBase -= ClickEvents_ClickTowerBase;
        ClickEvents.ClickTower -= ClickEvents_ClickTower;
    }

    public void ClickEvents_ClickTowerBase(TowerBase towerBase)
    {
        ClickEvents.ClickTowerBaseFlag = true;
    }


    public void ClickEvents_ClickTower(Tower tower)
    {
        ClickEvents.ClickTowerFlag = true;
    }

    public void UnClickEventTrigger()
    {
        if (!ClickEvents.ClickTowerBaseFlag) ClickEvents.UnClickTowerBase?.Invoke();
        if (!ClickEvents.ClickTowerFlag) ClickEvents.UnClickTower?.Invoke();

        ClickEvents.ClickTowerBaseFlag = false;
        ClickEvents.ClickTowerFlag = false;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (StateManager.Instance.CurrentState == StateType.GameplayState)
            {
                GameObject obj = null;
                if (!Utils.GetRaycast2D(Input.mousePosition, out obj))
                {
                    Debug.Log(obj);
                    if (obj != null)
                    {
                        IClickable clickable = obj.GetComponent<IClickable>();
                        if (clickable != null)
                        {
                            clickable.OnClick();
                        }
                    }
                    UnClickEventTrigger();
                }
            }
        }
    }
}
