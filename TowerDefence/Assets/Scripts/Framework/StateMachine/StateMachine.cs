using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Framework
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        public virtual void SetCurrentState(IState state)
        {
            //Debug.Log(state.Name);

            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            if (CurrentState != null && m_CurrentPlayCoroutine != null)
            {
                //interrupt currently executing state
                Skip();
            }

            CurrentState = state;
            StateManager.Instance.StartStateCoroutine(Play());
        }
        
        Coroutine m_CurrentPlayCoroutine;
        bool m_PlayLock;

        IEnumerator Play()
        {
            if (!m_PlayLock)
            {
                m_PlayLock = true;

                CurrentState.Enter();

                m_CurrentPlayCoroutine = StateManager.Instance.StartStateCoroutine(CurrentState.Execute());
                yield return m_CurrentPlayCoroutine;

                m_CurrentPlayCoroutine = null;
            }
        }

        void Skip()
        {
            if (CurrentState == null)
                throw new Exception($"{nameof(CurrentState)} is null!");

            if (m_CurrentPlayCoroutine != null)
            {
                StateManager.Instance.StopStateCoroutine(ref m_CurrentPlayCoroutine);
                //finalize current state
                CurrentState.Exit();
                m_CurrentPlayCoroutine = null;
                m_PlayLock = false;
            }
        }

        public virtual void Run(IState state)
        {
            SetCurrentState(state);
            CurrentState.ResetRaiseStates();
            Run();
        }

        Coroutine m_LoopCoroutine;

        public virtual void Run()
        {
            if (m_LoopCoroutine != null) //already running
                return;
          
            m_LoopCoroutine = StateManager.Instance.StartStateCoroutine(Loop());
        }

        protected virtual IEnumerator Loop()
        {
            while (true)
            {
                if (CurrentState != null && m_CurrentPlayCoroutine == null) //current state is done playing
                {
                    if (CurrentState.ValidateLinks(out var nextState))
                    {
                        if (m_PlayLock)
                        {
                            //finalize current state
                            CurrentState.Exit();
                            m_PlayLock = false;
                        }
                        CurrentState.ResetRaiseStates();
                        SetCurrentState(nextState);
                        CurrentState.ResetRaiseStates();
                    }
                }

                yield return null;
            }
        }
    }
}
