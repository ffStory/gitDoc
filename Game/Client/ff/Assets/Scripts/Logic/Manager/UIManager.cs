using System;
using System.Collections.Generic;
using UI.Views;
using UnityEngine;

namespace Logic.Manager
{
    public class ViewConfig
    {
        public ViewConfig(string name, string path, ViewLayer layer)
        {
            Name = name;
            Path = path;
            Layer = layer;
        }

        public readonly string Name;
        public readonly string Path;
        public readonly ViewLayer Layer;
    }

    /// <summary>
    /// A跳转B界面 B返回A带回的参数
    /// </summary>
    public class ViewBackData
    {
        public UIType Type;
    }
    
    public class BaseUISnapShoot
    {
        public ViewDeActiveType ViewDeActivePushType = ViewDeActiveType.Destroy; // A跳转B界面 A的关闭方式
        public ViewDeActiveType ViewDeActiveBackType = ViewDeActiveType.Destroy;// A跳转B界面 B返回关闭方式
        private readonly Stack<UIType> _uiList; // 一个View里面子UI栈
        private readonly ViewConfig _viewConfig;
        public ViewBackData ViewBackData;

        public BaseUISnapShoot(UIType type)
        {
            ResManager.Instance.UIConfigResMapMsg.Map.TryGetValue(type.ToString(), out var uiConfig);
            if (uiConfig == null)
            {
                Debug.LogError($"{type} UIConfig is null");
                return;
            }
            _viewConfig = UIManager.Instance.GetViewConfig(uiConfig.View);
            _uiList = new Stack<UIType>();
            PushUI(type);
        }

        public ViewConfig GetViewConfig()
        {
            return _viewConfig;
        }

        public UIType GetTopUI()
        {
            return _uiList.Peek();
        }

        public void PushUI(UIType viewType)
        {
            _uiList.Push(viewType);
        }

        public void ReplaceUI(UIType viewType)
        {
            _uiList.Pop();
            PushUI(viewType);
        }

        public UIType BackUI()
        {
            _uiList.Pop();
            return _uiList.Peek();
        }

        public int GetUICount()
        {
            return _uiList.Count;
        }

        public bool CheckLastUI()
        {
            return GetUICount() == 1;
        }
    }

    public class UIManager
    {
        private UIManager()
        {
            _snapShoots = new Stack<BaseUISnapShoot>();
            _cacheViews = new Dictionary<string, BaseView>();
            _layers = new Dictionary<ViewLayer, Transform>();
            _viewConfigs = new Dictionary<string, ViewConfig>();
            Init();
        }

        private void Init()
        {
            var prefab = Resources.Load<Transform>("UI/Prefab/Items/Canvas");
            var canvas = UnityEngine.Object.Instantiate(prefab);
            canvas.name = "Canvas";
            UnityEngine.Object.DontDestroyOnLoad(canvas);

            var layers = canvas.Find("Layers");
            for (var i = 0; i < layers.childCount; i++)
            {
                var layer = layers.GetChild(i);
                var layerType = (ViewLayer)Enum.Parse(typeof(ViewLayer), layer.name);
                _layers.Add(layerType, layer);
            }

            RegisterView();
        }
        
        private void RegisterView()
        {
            RegisterView(HeroListView.Name, HeroListView.Path);
            RegisterView(HeroInfoView.Name, HeroInfoView.Path);
        }

        private void RegisterView(string name, string path, ViewLayer layer = ViewLayer.Bottom)
        {
            _viewConfigs.Add(name, new ViewConfig(name, path, layer));
        }


        /// <summary>
        /// 关闭上个界面，跳转到下个界面
        /// ui快照入栈
        /// </summary>
        /// <param name="snapShoot"></param>
        public void PushView(BaseUISnapShoot snapShoot)
        {
            //上个UI的处理方式，根据ViewDeActivePushType
            if (_snapShoots.Count > 0)
            {
                DeActiveView(_snapShoots.Peek().GetViewConfig().Name, snapShoot.ViewDeActivePushType);
            }
            
            _snapShoots.Push(snapShoot);
            ShowView(snapShoot);
        }

        /// <summary>
        /// 使一个View失效。三种方式
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="closeType"></param>
        private void DeActiveView(string viewName, ViewDeActiveType closeType)
        {
            _cacheViews.TryGetValue(viewName, out BaseView viewBase);
            if (viewBase == null){return;} 
            switch (closeType)
            {
                case ViewDeActiveType.Destroy:
                case ViewDeActiveType.Disable:
                    CloseView(viewName, closeType);
                    break;
                case ViewDeActiveType.None:
                    viewBase.Dropdown();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 显示一个View
        /// </summary>
        /// <param name="snapShoot"></param>
        private void ShowView(BaseUISnapShoot snapShoot)
        {
            var config = snapShoot.GetViewConfig();
            _cacheViews.TryGetValue(config.Name, out BaseView baseView);
            var isActive = baseView != null && baseView.gameObject.activeSelf;
            if (baseView == null)
            {
                var prefab = Resources.Load<Transform>(config.Path);
                var tfm = UnityEngine.Object.Instantiate(prefab, _layers[config.Layer]);
                tfm.name = config.Name;
                baseView = tfm.GetComponent<BaseView>();
                _cacheViews.Add(config.Name, baseView);
                baseView.Init(snapShoot);
            }
            baseView.transform.SetAsLastSibling();
            baseView.gameObject.SetActive(true);
            if (!isActive)
            {
                baseView.Open(snapShoot); 
            }
            else
            {
                baseView.RaiseUp();
            }
        }

        private bool CheckLastView()
        {
            return _snapShoots.Count == 1;
        }

        public bool CheckLastUI()
        {
            return CheckLastView() && GetTopSnapShoot().CheckLastUI();
        }
        
        /// <summary>
        /// 返回UI，可以是同一个View中的返回
        /// </summary>
        /// <param name="viewBackData"></param>
        public void BackUI(ViewBackData viewBackData = null)
        {
            //返回上一个UI
            if (CheckLastUI()){return;}
            var topSnap = GetTopSnapShoot();
            if (topSnap.CheckLastUI())
            {
                var config = _snapShoots.Peek().GetViewConfig();
                DeActiveView(config.Name, topSnap.ViewDeActiveBackType);
                _snapShoots.Pop();
                var snap = _snapShoots.Peek();
                if (viewBackData != null) {snap.ViewBackData = viewBackData;}
                ShowView(snap); 
            }
            else
            {
                var topView = GetTopView();
                topView.Back();
            }
        }
        
        /// <summary>
        /// 返回上一个View
        /// </summary>
        /// <param name="viewBackData"></param>
        public void BackView(ViewBackData viewBackData = null)
        {
            //最后的UI 没有返回键
            if (CheckLastUI()){return;}
            var config = _snapShoots.Peek().GetViewConfig();
            var topSnap = GetTopSnapShoot();
            DeActiveView(config.Name, topSnap.ViewDeActiveBackType);
            _snapShoots.Pop();
            var snap = _snapShoots.Peek();
            if (viewBackData != null) {snap.ViewBackData = viewBackData;}
            ShowView(snap); 
        }

        public BaseUISnapShoot GetTopSnapShoot()
        {
            return _snapShoots.Peek();
        }

        public BaseView GetTopView()
        {
            if (_snapShoots.Count <= 0){return null;}

            var topSnapShoot = _snapShoots.Peek();
            _cacheViews.TryGetValue(topSnapShoot.GetViewConfig().Name, out BaseView viewBase);
            return viewBase;
        }

        private void CloseView(string viewName, ViewDeActiveType closeType)
        {
            _cacheViews.TryGetValue(viewName, out BaseView viewBase);
            if (viewBase == null){return;}
            viewBase.Close();
            switch (closeType)
            {
                case ViewDeActiveType.Destroy:
                    viewBase.Destroy();
                    _cacheViews.Remove(viewName);
                    UnityEngine.Object.Destroy(viewBase.gameObject);
                    break;
                case ViewDeActiveType.Disable:
                    viewBase.Disable();
                    viewBase.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ViewConfig GetViewConfig(string viewName)
        {
            _viewConfigs.TryGetValue(viewName, out var config);
            return config;
        }

        private static UIManager _instance;
        public static UIManager Instance => _instance ??= new UIManager();
        private readonly Stack<BaseUISnapShoot> _snapShoots;
        private readonly Dictionary<ViewLayer, Transform> _layers;
        private readonly Dictionary<string, BaseView> _cacheViews;
        private readonly Dictionary<string, ViewConfig> _viewConfigs;
    }
}