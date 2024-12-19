using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Data/EnemySO"), fileName = ("Enemy_"))]
public class EnemySO : ScriptableObject
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] int _enemyId;
    [SerializeField] string _enemyName;
    [SerializeField] int _gold;

    public GameObject EnemyPrefab => _enemyPrefab;
    public int EnemyId => _enemyId;
    public string EnemyName => _enemyName;
    public int Gold => _gold;

}
