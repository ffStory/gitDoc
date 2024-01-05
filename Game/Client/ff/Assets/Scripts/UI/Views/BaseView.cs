using Logic.Manager;
using UI.Item;
using UnityEngine;

namespace UI.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        private BaseUISnapShoot _snapShoot;
        private TopNavigation _navigation;
        private ViewBackData _viewBackData;

        private UIType TopUI => _snapShoot.GetTopUI();

        protected BaseUISnapShoot GetSnapShoot()
        {
            return _snapShoot;
        }
        
        protected void Push(UIType type)
        {
            if (!CheckUIConfig(type)){return;}
            _snapShoot.PushUI(type);
            RefreshView();
        }
        protected void Replace(UIType type)
        {
            if (!CheckUIConfig(type)){return;}
            _snapShoot.ReplaceUI(type);
            RefreshView();
        }

        private bool CheckUIConfig(UIType type)
        {
            ResManager.Instance.UIConfigResMapMsg.Map.TryGetValue(type.ToString(), out var config);
            if (config == null || config.View != _snapShoot.GetViewConfig().Name)
            {
                Debug.LogError("找不到配置或者不同View预制");
                return false;
            }

            return true;
        }
        
        public void Back()
        {
            _snapShoot.BackUI();
            RefreshView();
        }

        public virtual void Init(BaseUISnapShoot snapShoot)
        {
            _snapShoot = snapShoot;
            
            var prefab = Resources.Load<GameObject>("UI/Prefab/Items/TopNavigation");
            if (prefab != null)
            {
                var obj = Instantiate(prefab, transform);
                obj.name = "TopNavigation";
                _navigation = obj.transform.GetComponent<TopNavigation>();
                _navigation.Init(this);
                _viewBackData = new ViewBackData();
            }
        }

        public virtual void Open(BaseUISnapShoot snapShoot)
        {
            _snapShoot = snapShoot;
            UpdateTopNavigation();
        }

        public virtual void RaiseUp()
        {
            Debug.Log("RaiseUp" + _snapShoot.GetViewConfig().Name);
            UpdateTopNavigation();
        }

        public virtual void Dropdown()
        {
            Debug.Log("Dropdown" + _snapShoot.GetViewConfig().Name);
        }
        
        
        public virtual void Destroy()
        {
            Debug.Log("Destroy" + _snapShoot.GetViewConfig().Name);
        }
        
        
        public virtual void Disable()
        {
            Debug.Log("Disable" + _snapShoot.GetViewConfig().Name);
 
        }
        
        public virtual void Close()
        {
            Debug.Log("Close" + _snapShoot.GetViewConfig().Name);
        }
        
        public virtual void Show()
        {
            Debug.Log("Hide" + _snapShoot.GetViewConfig().Name);
        }

        protected virtual void RefreshView()
        {
        }

        private void UpdateTopNavigation()
        {
            ResManager.Instance.UIConfigResMapMsg.Map.TryGetValue(TopUI.ToString(), out var config);
            if (config == null)
            {
                Debug.LogError($"{TopUI} UIConfig is null in UpdateTopNavigation");
                return;
            }
            
            _navigation.Refresh(config);
        }

        public ViewBackData GetViewBackData()
        {
            return _viewBackData;
        }
    }
}
