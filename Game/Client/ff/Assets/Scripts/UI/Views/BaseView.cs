using Logic.Manager;
using UnityEngine;

namespace UI.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        private BaseUISnapShoot _snapShoot;
        private UIType TopUI => _snapShoot.GetTopUI();

        protected void Jump(UIType type)
        {
            _snapShoot.PushUI(type);
            RefreshView();
        }
        protected void Replace(UIType type)
        {
            _snapShoot.ReplaceUI(type);
            RefreshView();
        }
        
        public void Back()
        {
            _snapShoot.BackUI();
            RefreshView();
        }


        public virtual void Open(BaseUISnapShoot snapShoot)
        {
            _snapShoot = snapShoot;
        }
        
        public virtual void Close()
        {
            
        }

        protected virtual void RefreshView()
        {
            UIManager.Instance.UpdateTopNavigation(TopUI);
        }
        


    }
}
