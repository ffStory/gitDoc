using Logic.Manager;
using UnityEngine;

namespace UI.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        private BaseUISnapShoot _snapShoot;
        private UIType TopUI => _snapShoot.GetTopUI();

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

        public virtual void Create(BaseUISnapShoot snapShoot)
        {
            _snapShoot = snapShoot;
        }

        public virtual void Open(BaseUISnapShoot snapShoot)
        {
            _snapShoot = snapShoot;
        }

        public virtual void RaiseUp()
        {
            Debug.Log("RaiseUp" + _snapShoot.GetViewConfig().Name);
        }

        public virtual void Dropdown()
        {
            UIManager.Instance.UpdateTopNavigation(TopUI);
 
            Debug.Log("Dropdown" + _snapShoot.GetViewConfig().Name);
        }
        
        
        public virtual void Destroy()
        {
            Debug.Log("Destroy" + _snapShoot.GetViewConfig().Name);
            UIManager.Instance.UpdateTopNavigation(TopUI);
        }
        
        
        public virtual void Disable()
        {
            Debug.Log("Disable" + _snapShoot.GetViewConfig().Name);
 
        }
        
        public virtual void Close()
        {
            Debug.Log("Close" + _snapShoot.GetViewConfig().Name);
 
        }

        protected virtual void RefreshView()
        {
            UIManager.Instance.UpdateTopNavigation(TopUI);
        }
        


    }
}
