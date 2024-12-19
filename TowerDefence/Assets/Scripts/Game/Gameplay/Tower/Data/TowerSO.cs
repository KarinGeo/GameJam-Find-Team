using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Data/TowerSO"), fileName = ("Tower"))]
public class TowerSO : ScriptableObject
{
    [SerializeField] int _id;
    [SerializeField] TowerType _towerType;
    [SerializeField] GameObject _prefab;
    [SerializeField] GameObject _obj;
    [SerializeField] List<Sprite> _level;
    [SerializeField] List<int> _cost;
    [SerializeField] List<int> _sale;
    

    public int Id => _id;
    public TowerType TowerType => _towerType;
    public GameObject Prefab => _prefab;
    public GameObject Obj => _obj;
    public List<Sprite> Level => _level;
    public List<int> Cost => _cost;
    public List<int> Sale => _sale;

}
