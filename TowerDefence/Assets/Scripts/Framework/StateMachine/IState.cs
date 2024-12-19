using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IState
    {
        string Name { get; set; }

        StateType Type { get; }

        void Enter();

        IEnumerator Execute();

        void Exit();

        void AddLink(ILink link);

        bool ValidateLinks(out IState nextState);

        void ResetRaiseStates();

        StateType GetStateType();
    }
}

