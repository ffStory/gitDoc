using System;
using System.Collections.Generic;
using UI.Item;
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
    
    public class BaseUISnapShoot
    {
        public ViewCloseType ViewJumpCloseType = ViewCloseType.Destroy; // A跳转B界面 A的关闭方式
        public ViewCloseType ViewBackCloseType = ViewCloseType.Destroy;// A跳转B界面 B返回关闭方式
        private readonly Stack<UIType> _uiList;
        private readonly ViewConfig _viewConfig;

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

        public ViewConfig GetConfig()
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

        public int GetViewCount()
        {
            return _uiList.Count;
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

            _topNavigation = canvas.Find("TopNavigation").GetComponent<TopNavigation>();
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
        public void JumpView(BaseUISnapShoot snapShoot)
        {
            if (_snapShoots.Count > 0)
            {
                CloseView(_snapShoots.Peek().GetConfig().Name, snapShoot.ViewJumpCloseType);
            }
            
            _snapShoots.Push(snapShoot);
            OpenView(snapShoot);
        }

        private void OpenView(BaseUISnapShoot snapShoot)
        {
            var config = snapShoot.GetConfig();
            _cacheViews.TryGetValue(config.Name, out BaseView baseView);
            if (baseView == null)
            {
                var prefab = Resources.Load<Transform>(config.Path);
                var tfm = UnityEngine.Object.Instantiate(prefab, _layers[config.Layer]);
                tfm.name = config.Name;
                baseView = tfm.GetComponent<BaseView>();
                _cacheViews.Add(config.Name, baseView);
            }
            baseView.transform.SetAsLastSibling();
            baseView.gameObject.SetActive(true);
            baseView.Open(snapShoot); 
        }

        public void BackView()
        {
            if (_snapShoots.Count == 1 && _snapShoots.Peek().GetViewCount() == 1){return;}
            var snapShoot = _snapShoots.Peek();
            if (snapShoot.GetViewCount() <= 1)
            {
                var config = _snapShoots.Peek().GetConfig();
                CloseView(config.Name, snapShoot.ViewBackCloseType);
                _snapShoots.Pop();
                OpenView(_snapShoots.Peek()); 
            }
            else
            {
                var topView = GetTopView();
                topView.Back();
            }
        }

        public BaseView GetTopView()
        {
            if (_snapShoots.Count <= 0){return null;}

            var topSnapShoot = _snapShoots.Peek();
            _cacheViews.TryGetValue(topSnapShoot.GetConfig().Name, out BaseView viewBase);
            return viewBase;
        }

        private void CloseView(string viewName, ViewCloseType closeType)
        {
            _cacheViews.TryGetValue(viewName, out BaseView viewBase);
            if (viewBase == null){return;}
            viewBase.Close();
            switch (closeType)
            {
                case ViewCloseType.Destroy:
                    _cacheViews.Remove(viewName);
                    UnityEngine.Object.Destroy(viewBase.gameObject);
                    break;
                case ViewCloseType.Hide:
                    viewBase.gameObject.SetActive(false);
                    break;
                case ViewCloseType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateTopNavigation(UIType type)
        {
            ResManager.Instance.UIConfigResMapMsg.Map.TryGetValue(type.ToString(), out var config);
            if (config == null)
            {
                Debug.LogError($"{type} UIConfig is null in UpdateTopNavigation");
                return;
            }
            
            _topNavigation.Refresh(config);
        }

        public ViewConfig GetViewConfig(string viewName)
        {
            _viewConfigs.TryGetValue(viewName, out var config);
            return config;
        }

        private TopNavigation _topNavigation;
        private static UIManager _instance;
        public static UIManager Instance => _instance ??= new UIManager();
        private readonly Stack<BaseUISnapShoot> _snapShoots;
        private readonly Dictionary<ViewLayer, Transform> _layers;
        private readonly Dictionary<string, BaseView> _cacheViews;
        private readonly Dictionary<string, ViewConfig> _viewConfigs;
    }
}