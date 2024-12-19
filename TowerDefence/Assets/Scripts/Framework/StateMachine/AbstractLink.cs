using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public abstract class AbstractLink : ILink
    {
        public abstract bool Validate(out IState nextState);

        public virtual void ResetRaiseState()
        {
            
        }
    }
}

