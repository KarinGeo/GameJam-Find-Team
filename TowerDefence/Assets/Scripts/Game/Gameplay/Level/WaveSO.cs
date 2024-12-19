using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Data/WaveSO"), fileName = ("Wave_"))]
public class WaveSO : ScriptableObject
{
    [SerializeField] EnemySO enemy;
    [SerializeField] int enemyLevel;
    [SerializeField] float frequency;
    [SerializeField] int count;
    [SerializeField] int pathIndex;
    [SerializeField] float nextWaveSpan;

    public EnemySO Enemy => enemy;
    public int EnemyLevel => enemyLevel;
    public float Frequency => frequency;
    public int Count => count;
    public int PathIndex => pathIndex;
    public float NextWaveSpan => nextWaveSpan;
}
