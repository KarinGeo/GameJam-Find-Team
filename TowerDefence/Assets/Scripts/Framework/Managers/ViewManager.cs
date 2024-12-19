using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Device;
using System.Linq;
using static UnityEditor.PlayerSettings;

namespace Framework
{
    public class ViewInfo
    {
        private string _prefabName;
        private Transform _parentTf;
        private BaseController _controller;

        public string PrefabName { get { return _prefabName; } set { _prefabName = value; } }
        public Transform ParentTf { get { return _parentTf; } set { _parentTf = value; } }
        public BaseController Controller { get { return _controller; } set { _controller = value; } }
    }

    //统一管理视图
    //Canvas用来显示普通视图，跳转，有返回键的视图，需要处理上下关系。执行非链接性跳转时新建视图。
    //WorldCanvas用来显示没有返回键，点击空白处隐藏的视图, 定时销毁的视图等。
    //Canvas普通视图使用Open和Return
    //WorldCanvas视图使用Show和Hide
    public class ViewManager : UnitySingleton<ViewManager>
    {

        private Transform _canvasTf;
        private Transform _worldCanvasTf;

        private Dictionary<int, ViewInfo> _views; //注册的视图

        public Transform CanvasTf { get { return _canvasTf; } set { _canvasTf = value; } }
        public Transform WorldCanvasTf { get { return _worldCanvasTf; } set { _worldCanvasTf = value; } }

        private Stack<IBaseView> _canvasOpens;               //已打开的视图栈
        private Dictionary<int, IBaseView> _worldCanvasCaches;    //已缓存的视图字典


        public override void Awake()
        {
            base.Awake();
            CanvasTf = GameObject.Find("Canvas").transform;
            WorldCanvasTf = GameObject.Find("WorldCanvas").transform;
            _views = new Dictionary<int, ViewInfo>();
            _canvasOpens = new Stack<IBaseView>();
            _worldCanvasCaches = new Dictionary<int, IBaseView>();
            SubscribeToEvents();
        }

        public void Register(int key, ViewInfo viewInfo)
        {
            if (_views.ContainsKey(key) == false)
            {
                _views.Add(key, viewInfo);
            }
        }

        public void Register(ViewType viewType, ViewInfo viewInfo)
        {
            Register((int)viewType, viewInfo);
        }

        public void Unregister(int key)
        {
            if (_views.ContainsKey(key))
            {
                _views.Remove(key);
            }
        }

        public void Unregister(ViewType viewType)
        {
            if (_views.ContainsKey((int)viewType))
            {
                _views.Remove((int)viewType);
            }
        }

        public void Open(ViewType type, params object[] args)
        {
            Open((int)type, args);
        }

        public void Open(int key, params object[] args)
        {
            IBaseView view = InstantiateNewView(key, args);
            Show(view, args);
        }

        public IBaseView InstantiateNewView(int key, params object[] args)
        {
            IBaseView view;
            ViewInfo viewInfo = _views[key];

            string type = ((ViewType)key).ToString(); //类型的字符串跟脚本名称对应
            GameObject uiObj = UnityEngine.Object.Instantiate(ResourceManager.Instance.GetAssetCache<GameObject>("UI/UIPrefabs/" + type + ".prefab"), viewInfo.ParentTf) as GameObject;

            Canvas canvas = uiObj.GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = uiObj.AddComponent<Canvas>();
            }
            if (uiObj.GetComponent<GraphicRaycaster>() == null)
            {
                uiObj.AddComponent<GraphicRaycaster>();
            }


            canvas.overrideSorting = true; //可以设置层级

            view = uiObj.AddComponent(Type.GetType(type)) as IBaseView; //添加对应View脚本
            view.ViewId = key;
            view.Controller = viewInfo.Controller;
            view.ParentTf = viewInfo.ParentTf;


            view.Init(args);

            if (view.ParentTf == _canvasTf)
            {
                _canvasOpens.Push(view);
            }
            else if (view.ParentTf == _worldCanvasTf)
            {
                _worldCanvasCaches.Add(view.ViewId, view);
            }

            return view;
        }

        public void Show(IBaseView view, string ani, params object[] args)
        {
            Show(view, args);
            PlayAnimation(view, ani);
        }

        public void Show(IBaseView view, params object[] args)
        {
            if (view == null)
                return;

            BaseView baseView = view as BaseView;

            Canvas canvas = baseView.gameObject.GetComponent<Canvas>();
            canvas.overrideSorting = true;
            canvas.sortingOrder = _canvasOpens.Count + _worldCanvasCaches.Count + 1;


            view.Show(args);
        }

        public void Show(ViewType type, params object[] args)
        {
            if (_worldCanvasCaches.ContainsKey((int)type))
            {
                Show(_worldCanvasCaches[(int)type], args);
            }
            else
            {
                Open(type, args);
            }
        }

        public void Show(ViewType type, string ani, params object[] args)
        {
            Show(type, args);
            PlayAnimation(type, ani);
        }

        public void Show(ViewType type, Vector3 pos, string ani, params object[] args)
        {
            Show(type, args);
            SetPosition(type, pos);
            PlayAnimation(type, ani);
        }

        public void Hide(IBaseView view)
        {
            if (view == null)
                return;

            view.Hide();
        }

        public void Hide(ViewType type)
        {
            if (_worldCanvasCaches.ContainsKey((int)type))
            {
                Hide(_worldCanvasCaches[(int)type]);
            }
        }

        public void SetPosition(ViewType type, Vector3 pos)
        {
            if (_worldCanvasCaches.ContainsKey((int)type))
            {
                (_worldCanvasCaches[(int)type] as BaseView).transform.position = pos;   
            }
        }

        public void PlayAnimation(ViewType type, string ani)
        {
            if (_worldCanvasCaches.ContainsKey((int)type))
            {
                Animator animator = (_worldCanvasCaches[(int)type] as BaseView).GetComponent<Animator>();
                if (animator != null)
                {
                    animator.Play(ani, -1, 0f);
                }
            }
        }

        public void PlayAnimation(IBaseView view, string ani)
        {
            Animator animator = (view as BaseView).GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play(ani, -1, 0f);
            }
        }








        public void SubscribeToEvents()
        {
            UIEvents.ViewReturn += UIEvents_ViewReturn;
            UIEvents.AllOpenViewsClose += CloseAllOpenViews;
        }

        public void CloseAllOpenViews()
        {
            foreach (var view in _canvasOpens)
            {
                view.Close();
            }

            foreach (var view in _worldCanvasCaches)
            {
                view.Value.Close();
            }

            _canvasOpens.Clear();
            _worldCanvasCaches.Clear();

        }

        public void UIEvents_ViewReturn()
        {
            if (_canvasOpens.Count != 0)
            {
                _canvasOpens.Peek().Close();
                Show(_canvasOpens.Pop());
            }
        }
    }

}
    