using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Framework
{
    public class State : AbstractState
    {
        readonly Action m_OnExecute;

        public State(Action onExecute,StateType type, string stateName = nameof(State))
        {
            m_OnExecute = onExecute;
            Name = stateName;
            Type = type;
        }

        public override IEnumerator Execute()
        {
            yield return null;

            m_OnExecute?.Invoke();
        }
    }
}