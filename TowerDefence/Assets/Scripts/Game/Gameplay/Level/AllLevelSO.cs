using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = ("Data/AllLevelSO"), fileName = ("AllLevels"))]
public class AllLevelSO : ScriptableObject
{
    [SerializeField] List<LevelSO> levels;

    public List<LevelSO> Levels => levels;
}

