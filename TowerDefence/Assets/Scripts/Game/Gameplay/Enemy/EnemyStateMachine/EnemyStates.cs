using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Framework;
public class EnemyState : IEnemyState
{
    private EnemyStateMachine _stateMachine;
    private Enemy _unit;
    protected EnemyStateType _type;

    public EnemyStateMachine StateMachine { get { return _stateMachine; }}
    public Enemy Unit { get { return _unit; }}

    public EnemyState(EnemyStateMachine stateMachine, Enemy unit)
    {
        _stateMachine = stateMachine;
        _unit = unit;
    }

    public virtual void OnEnter()
    {
        
    }

    public virtual void OnExit()
    {
        
    }

    public virtual void OnUpdate()
    {
        
    }

    EnemyStateType IEnemyState.GetType()
    {
        return _type;
    }
}

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyStateMachine stateMachine, Enemy unit) : base(stateMachine, unit)
    {
        _type = EnemyStateType.Idle;
    }
}

public class EnemyRunState : EnemyState
{

    public EnemyRunState(EnemyStateMachine stateMachine, Enemy unit) : base(stateMachine, unit)
    {
        _type = EnemyStateType.Run;
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        Unit.Animator.Play("Run");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

  

        if (Unit.Index < Unit.Path.Count)
        {
            Unit.transform.Translate(Utils.GetVector3((Utils.GetVector2(Unit.Path[Unit.Index]) - Utils.GetVector2(Unit.transform.position)).normalized) * Unit.Speed * Time.deltaTime);
            if (Vector2.Distance(new Vector2(Unit.Path[Unit.Index].x, Unit.Path[Unit.Index].y), new Vector2(Unit.transform.position.x, Unit.transform.position.y)) < 0.1f)
            {
                Unit.Index++;
            }
        }
        else
        {
            Unit.DisableCollider();
            Unit.Destroy(0f);
            GameEvents.HealthDown?.Invoke();
        }
    }
}

public class EnemyDieState : EnemyState
{
    public EnemyDieState(EnemyStateMachine stateMachine, Enemy unit) : base(stateMachine, unit)
    {
        _type = EnemyStateType.Die;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        GameEvents.GoldUp?.Invoke(Unit.Gold);
        Unit.Destroy(0.5f);
        Unit.DisableCollider();
        Unit.Animator.Play("Die");
    }



    

}


