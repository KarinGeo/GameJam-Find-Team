using Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour, IAttackable
{
    private List<Vector3> _path;
    private EnemyStateMachine _stateMachine;
    private Animator _animator;
    private Collider2D _collider;
    private bool _isDie = false;
    private int _gold;


    private float _maxHp;
    [SerializeField]
    private float _currentHp;
    private float _baseSpeed = 75f;
    private float _speed;

    public bool IsDie { get { return _isDie; } }

    private int _index = 0;

    public List<Vector3> Path => _path;
    public Animator Animator => _animator;
    public float Speed => _speed;
    public int Gold => _gold;

    public int Index { get { return _index; } set { _index = value; } }
    public float CurrentHp { get { return _currentHp; } set { _currentHp = value; } }

    public void Awake()
    {
        _stateMachine = new EnemyStateMachine(this);
        _animator = transform.Find("Body").GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _stateMachine.RegisterState(EnemyStateType.Run, new EnemyRunState(_stateMachine, this));
        _stateMachine.RegisterState(EnemyStateType.Die, new EnemyDieState(_stateMachine, this));
    }

    private void Update()
    {
        _stateMachine.Update();

        if (_currentHp <= 0 && _stateMachine.CurrentStateType != EnemyStateType.Die)
        {
            _stateMachine.TransitionState(EnemyStateType.Die);
            return;
        }
    }

    public void Init(List<Vector3> path, EnemySO data, int level)
    { 
        this._path = path;
        _speed = _baseSpeed;

        ConfigData enemyData = ConfigManager.Instance.GetConfigData("EnemyData");
        int enemyDataId = int.Parse(data.EnemyId.ToString() + Utils.FormatNumber(level, 2));
        _maxHp = float.Parse(enemyData.GetDataById(enemyDataId)["HP"]);
        _currentHp = _maxHp;
        _speed = _baseSpeed * float.Parse(enemyData.GetDataById(enemyDataId)["Speed"]);
        _gold = data.Gold;
        _stateMachine.TransitionState(EnemyStateType.Run);
    }

    public void Destroy(float delay)
    {
        Destroy(gameObject, delay);
    }

    public void GetHurt(float damage)
    {
        _currentHp -= damage;
    }

    public void DisableCollider()
    {
        _isDie = true;
        _collider.enabled = false;
    }
}
