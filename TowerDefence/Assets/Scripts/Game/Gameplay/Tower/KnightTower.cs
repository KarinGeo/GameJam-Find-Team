using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnightTower : Tower
{
    private Vector3 _spawnPoint;

    public override void Awake()
    {

        _spawnPoint = transform.Find("SpawnPoint").position;

    }
}
