using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StateManager : UnitySingleton<StateManager>
{
    StateMachine m_StateMachine = new StateMachine();

    MonoBehaviour s_CoroutineRunner;

    IState m_StartState;
    IState m_SelectLevelState;
    IState m_GameplayState;

    public StateType CurrentState => m_StateMachine.CurrentState.GetStateType();

    public IState CurrentStateInterface => m_StateMachine.CurrentState;

    private void Start()
    {
        //Coroutines.Init(this);
        s_CoroutineRunner = this;

        Init();
    }

    private void OnEnable()
    {
        SceneEvents.ExitApplication += SceneEvents_ExitApplication;
    }

    private void OnDisable()
    {
        SceneEvents.ExitApplication -= SceneEvents_ExitApplication;
    }

    public void Init()
    {
        
        SetStates();
        AddLinks();
        SubscribeToEvents();
        RunStateMachine();
    }

    public void RunStateMachine()
    {
        m_StateMachine.Run(m_StartState);
    }

    private void SetStates()
    {
        m_StartState = new State(null, StateType.StartState, "StartState");
        m_SelectLevelState = new State(null, StateType.SelectLevelState, "SelectLevelState");
        m_GameplayState = new State(null, StateType.GameplayState, "GameplayState");
    }

    private void AddLinks()
    {
        m_StartState.AddLink(new EventLink(ref StateEvents.ToSelectLevelState, m_SelectLevelState));
        m_SelectLevelState.AddLink(new EventLink(ref StateEvents.ToGameplayState, m_GameplayState));
    
    }

    private void SubscribeToEvents()
    {
        UIEvents.SelectLevelViewOpen += StateEvents.ToSelectLevelState;
        SceneEvents.LoadSceneByLevel += (i) => StateEvents.ToGameplayState();
    }


    public Coroutine StartStateCoroutine(IEnumerator coroutine)
    {
        if (s_CoroutineRunner == null)
        {
            throw new InvalidOperationException("CoroutineRunner is not initialized.");
        }

        return s_CoroutineRunner.StartCoroutine(coroutine);
    }

    public void StopStateCoroutine(Coroutine coroutine)
    {
        if (s_CoroutineRunner != null)
        {
            s_CoroutineRunner.StopCoroutine(coroutine);
        }
    }

    public void StopStateCoroutine(ref Coroutine coroutine)
    {
        if (s_CoroutineRunner != null && coroutine != null)
        {
            s_CoroutineRunner.StopCoroutine(coroutine);
            coroutine = null;
        }
    }


    private void SceneEvents_ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
