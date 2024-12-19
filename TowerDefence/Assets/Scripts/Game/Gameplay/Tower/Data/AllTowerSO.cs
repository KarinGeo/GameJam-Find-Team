using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = ("Data/AllTowerSO"), fileName = ("AllTowers"))]
public class AllTowerSO : ScriptableObject
{
    [SerializeField] List<TowerSO> towers;

    public List<TowerSO> Towers => towers;
}
