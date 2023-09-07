using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Views
{
    public enum ViewType
    {
        None = 0,
        Hero = 10,
        HeroList = 11,
        HeroList1 = 111,
        HeroInfo = 12
    }

    public enum ViewCloseType
    {
        Destroy = 1,
        Disable = 2
    }

    public class BaseUISnapShoot
    {
        public readonly string ViewName;
        public readonly bool ClosePreView;
        private readonly Stack<ViewType> _viewList;

        public BaseUISnapShoot(string viewName, ViewType viewType, bool closePreView = true)
        {
            ViewName = viewName;
            _viewList = new Stack<ViewType>();
            PushView(viewType);
            ClosePreView = closePreView;
        }

        public ViewType GetTopView()
        {
            return _viewList.Peek();
        }

        public void PushView(ViewType viewType)
        {
            _viewList.Push(viewType);
        }

        public void ReplaceView(ViewType viewType)
        {
            _viewList.Pop();
            PushView(viewType);
        }

        public ViewType BackView()
        {
            _viewList.Pop();
            return _viewList.Peek();
        }

        public int GetViewCount()
        {
            return _viewList.Count;
        }
    }

    public enum UILayer
    {
        Bottom = 0,
        Middle = 1,
        Top = 2
    }

    public class ViewConfig
    {
        public ViewConfig(string name, string path, UILayer layer, ViewCloseType closeType)
        {
            Name = name;
            Path = path;
            Layer = layer;
            ViewCloseType = closeType;
        }

        public readonly string Name;
        public readonly string Path;
        public readonly UILayer Layer;
        public readonly ViewCloseType ViewCloseType;
    }

    public class UIManager
    {
        private UIManager()
        {
            _snapShoots = new Stack<BaseUISnapShoot>();
            _layers = new Dictionary<UILayer, Transform>();
            _viewConfigs = new Dictionary<string, ViewConfig>();
            _cacheViews = new Dictionary<string, BaseView>();
            Init();
        }

        private void Init()
        {
            var prefab = Resources.Load<Transform>("UI/Prefab/Items/Canvas");
            var canvas = Object.Instantiate(prefab);
            canvas.name = "Canvas";
            Object.DontDestroyOnLoad(canvas);

            var layers = canvas.Find("Layers");
            for (var i = 0; i < layers.childCount; i++)
            {
                var layer = layers.GetChild(i);
                var layerType = (UILayer)Enum.Parse(typeof(UILayer), layer.name);
                _layers.Add(layerType, layer);
            }
            
            RegisterView();
        }

        private void RegisterView()
        {
            RegisterView(HeroListView.Name, HeroListView.Path, HeroListView.Layer, HeroListView.CloseType);
            RegisterView(HeroInfoView.Name, HeroInfoView.Path, HeroInfoView.Layer, HeroInfoView.CloseType);
        }

        private void RegisterView(string name, string path, UILayer layer, ViewCloseType closeType)
        {
            _viewConfigs.Add(name, new ViewConfig(name, path, layer, closeType));
        }

        /// <summary>
        /// 跳转UI
        /// ui快照入栈
        /// </summary>
        /// <param name="snapShoot"></param>
        public void JumpView(BaseUISnapShoot snapShoot)
        {
            if (snapShoot.ClosePreView && _snapShoots.Count > 0)
            {
                CloseView(_snapShoots.Peek().ViewName);
            }
            
            _snapShoots.Push(snapShoot);
            OpenView(snapShoot);
        }

        private void OpenView(BaseUISnapShoot snapShoot)
        {
            _cacheViews.TryGetValue(snapShoot.ViewName, out BaseView baseView);
            if (baseView == null)
            {
                var prefab = Resources.Load<Transform>(_viewConfigs[snapShoot.ViewName].Path);
                var tfm = Object.Instantiate(prefab, _layers[_viewConfigs[snapShoot.ViewName].Layer]);
                tfm.name = snapShoot.ViewName;
                baseView = tfm.GetComponent<BaseView>();
                _cacheViews.Add(snapShoot.ViewName, baseView);
            }
            baseView.transform.SetAsLastSibling();
            baseView.gameObject.SetActive(true);
            baseView.Open(snapShoot); 
        }

        public void BackView()
        {
            if (_snapShoots.Count <= 1){return;}
            CloseView(_snapShoots.Peek().ViewName);
            _snapShoots.Pop();
            OpenView(_snapShoots.Peek());
        }

        private void CloseView(string viewName)
        {
            _viewConfigs.TryGetValue(viewName, out ViewConfig viewConfig);
            _cacheViews.TryGetValue(viewName, out BaseView viewBase);
            
            if (viewConfig == null){return;}
            if (viewBase == null){return;}
            
            viewBase.Close();
            switch (viewConfig.ViewCloseType)
            {
                case ViewCloseType.Destroy:
                    _cacheViews.Remove(viewName);
                    Object.Destroy(viewBase.gameObject);
                    break;
                case ViewCloseType.Disable:
                    viewBase.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    
    
        private static UIManager _instance;
        public static UIManager Instance => _instance ??= new UIManager();

        private readonly Stack<BaseUISnapShoot> _snapShoots;
        private readonly Dictionary<UILayer, Transform> _layers;
        private readonly Dictionary<string, ViewConfig> _viewConfigs;
        private readonly Dictionary<string, BaseView> _cacheViews;
    }
}