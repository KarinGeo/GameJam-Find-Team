using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class BaseModel
    {
        private BaseController _controller;

        public BaseController Controller { get { return _controller; } set { _controller = value; } }
        public BaseModel(BaseController ctl)
        {
            Controller = ctl;
        }

        public BaseModel()
        {

        }

    }
}

