
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class ControllerManager : UnitySingleton<ControllerManager>
    {
        private Dictionary<int, BaseController> _modules;

        public override void Awake()
        {
            base.Awake();
            _modules = new Dictionary<int, BaseController>();
        }

        public void Register(ControllerType type, BaseController ctl)
        {
            Register((int)type, ctl);
        }

        public void Register(int controllerKey, BaseController ctl)
        {
            if (_modules.ContainsKey(controllerKey) == false)
            {
                _modules.Add(controllerKey, ctl);
            }
        }
    }
}
