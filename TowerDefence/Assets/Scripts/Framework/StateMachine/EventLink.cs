using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Framework;
using Unity.VisualScripting;

namespace Framework
{
    /// <summary>
    /// A link that listens for a specific event and becomes open for transition if the event is raised.
    /// If the current state is linked to next step by this link type,
    /// The state machine waits for the event to be triggered and then moves to the next step.
    /// </summary>
    public class EventLink : ILink
    {
        IState m_NextState;

        //Action m_GameEvent;
        bool m_EventRaised;

        // Pass a GameEvent (System.Action) and the next state into the Constructor.
        public EventLink(ref Action gameEvent, IState nextState)
        {

            //用下述方式重写一下构造函数试试，再把取消订阅的找到
            //eventPairs.Add(new EventPairWithParam<object>(
            //      eventA: obj => eventPair.EventA((T)obj),
            //      eventB: eventPair.EventB,
            //      shouldTriggerA: obj => eventPair.ShouldTriggerA((T)obj)
            //));

            gameEvent += OnEventRaised;
            m_NextState = nextState;
        }

        public bool Validate(out IState nextState)
        {
            nextState = null;
            bool result = false;

            if (m_EventRaised)
            {
                nextState = m_NextState;
                result = true;
            }

            return result;
        }

        public void OnEventRaised()
        {
            m_EventRaised = true;
        }

        public void ResetRaiseState()
        {
            m_EventRaised = false;
        }

    }
}
