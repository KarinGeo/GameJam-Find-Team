using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public abstract class AbstractState : IState
    {
        public virtual string Name { get; set; }

        public virtual StateType Type { get; set; }

        readonly List<ILink> m_Links = new();

        public void AddLink(ILink link)
        {
            if (!m_Links.Contains(link))
            {
                m_Links.Add(link);
            }
        }

        public virtual void Enter()
        {
        }

        public abstract IEnumerator Execute();

        public virtual void Exit()
        {
        }

        public virtual bool ValidateLinks(out IState nextState)
        {
            if (m_Links != null && m_Links.Count > 0)
            {
                foreach (var link in m_Links)
                {
                    var result = link.Validate(out nextState);
                    if (result)
                    {
                        return true;
                    }
                }
            }

            //default
            nextState = null;
            return false;
        }
        public void ResetRaiseStates()
        {
            foreach (var link in m_Links)
            {
                link.ResetRaiseState();
            }
        }

        public virtual StateType GetStateType()
        {
            return Type;
        }
    }
}