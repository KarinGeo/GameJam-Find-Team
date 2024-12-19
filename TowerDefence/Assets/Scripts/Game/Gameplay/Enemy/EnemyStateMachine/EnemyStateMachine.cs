using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;



public class EnemyStateMachine
{
    private Enemy _unit;
    private IEnemyState _currentState;
    private EnemyStateType _currentStateType;
    private Dictionary<EnemyStateType, IEnemyState> _states = new Dictionary<EnemyStateType, IEnemyState>();

    public EnemyStateType CurrentStateType => _currentStateType;

    public EnemyStateMachine(Enemy unit)
    {
        this._unit = unit;
    }

    public void Update()
    {
        _currentState.OnUpdate();
    }

    public void RegisterState(EnemyStateType type, IEnemyState state)
    {
        _states.Add(type, state);
    }

    public void TransitionState(EnemyStateType type)
    {
        _currentStateType = type;
        if (_currentState! != null)
            _currentState.OnExit();
        _currentState = _states[type];
        _currentState.OnEnter();
        _currentStateType = _currentState.GetType();
    }
}
