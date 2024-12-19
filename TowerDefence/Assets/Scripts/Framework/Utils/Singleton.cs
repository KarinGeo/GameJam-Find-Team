using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public abstract class Singleton<T> where T : new()
    {
        private static T _instance;
        private static object mutex = new object();
        protected MonoBehaviour s_CoroutineRunner;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }

        public virtual void SetCoroutineRunner(MonoBehaviour obj)
        { 
            this.s_CoroutineRunner = obj;
        }


        public virtual void ResetSelf()
        { 
            
        }
    }



    public class UnitySingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(T)) as T;
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = (T)obj.AddComponent(typeof(T));
                        obj.hideFlags = HideFlags.DontSave;
                        obj.name = typeof(T).Name;
                    }
                }
                return _instance;
            }
        }

        public virtual void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
        }

        public virtual void ResetSelf()
        { 
            
        }
    }

}