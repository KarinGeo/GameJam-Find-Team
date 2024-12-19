using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Framework;

public class GameplayView : BaseView
{
    Text _health;
    Text _gold;

    public override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        _health = transform.Find("Grid/Health/Text").GetComponent<Text>();
        _gold = transform.Find("Grid/Gold/Text").GetComponent<Text>();
        transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() => GameEvents.StartSpawnEnemy?.Invoke());
        UIEvents.GameplayViewRefresh += Refresh;
        UIEvents.GameplayViewRefresh?.Invoke();
    }

    private void OnDisable()
    {
        UIEvents.GameplayViewRefresh -= Refresh;
    }

    private void Refresh()
    {
        _health.text = GameplayManager.Instance.Health.ToString();
        _gold.text = GameplayManager.Instance.Gold.ToString();
    }
}
