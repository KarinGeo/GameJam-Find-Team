using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Framework
{
    public class BaseView : MonoBehaviour, IBaseView
    {
        public int ViewId { get; set; }
        public BaseController Controller { get; set; }
        public Transform ParentTf { get ; set ; }

        private bool _isInit = false;
        private bool _isShow = false;
        protected Canvas _canvas;

        protected Dictionary<string, GameObject> m_cache_gos = new Dictionary<string, GameObject>();

        public virtual void Awake()
        {
            _canvas = gameObject.GetComponent<Canvas>();
        }

        protected virtual void OnAwake()
        {

        }

        public bool IsInit()
        {
            return _isInit;
        }


        public bool IsShow()
        {
            return _isShow;
        }


        public void SetVisible(bool value)
        {
            _canvas.enabled = value;
        }

        public void Open()
        {
            
        }

        public void Close()
        {
            Destroy(this.gameObject);
        }

        public virtual void Init(params object[] args)
        {
            _isInit = true;
        }

        public GameObject Find(string res)
        {
            if (m_cache_gos.ContainsKey(res))
            {
                return m_cache_gos[res];
            }
            m_cache_gos.Add(res, transform.Find(res).gameObject);
            return m_cache_gos[res];
        }

        public T Find<T>(string res) where T : Component
        {
            GameObject obj = Find(res);
            return obj.GetComponent<T>();
        }

        public virtual void Show(params object[] args)
        {
            SetVisible(true);
        }

        public virtual void Hide()
        {
            SetVisible(false);
        }

        public virtual void Restore()
        {
            _isInit = false;
        }
    }
}