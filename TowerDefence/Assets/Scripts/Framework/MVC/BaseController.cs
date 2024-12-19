using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class BaseController
    {
        private BaseModel _model; //Ä£°åÊý¾Ý

        public BaseModel Model { get { return _model; } set { _model = value; } }

        public virtual void OpenView(IBaseView view)
        {

        }

        public virtual void RegisterViews()
        {

        }

        public virtual void SubscribeToEvents()
        { 
            
        }

        public virtual void UnsubscribeFromEvents()
        {

        }
    }
}

