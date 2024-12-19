using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Data/LevelSO"), fileName = ("Level_"))]
public class LevelSO : ScriptableObject
{
    [SerializeField] List<WaveSO> waves;

    public List<WaveSO> Waves => waves;
}
