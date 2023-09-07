using UnityEngine;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

namespace UI.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        private const string ViewTopStatePath = "UI/Prefab/Items/TopState";
        private BaseUISnapShoot _snapShoot;

        #region UI
        private Transform _topState;
        private Text _textTitle;
        private Button _btnBack;

        #endregion

        private void Awake()
        {
            var prefab = Resources.Load<Transform>(ViewTopStatePath);
            _topState = Instantiate(prefab, transform);
            _topState.name = "TopState";

            _textTitle = _topState.Find("Title").GetComponent<Text>();
            _btnBack = _topState.Find("Button").GetComponent<Button>();
        }

        private void OnEnable()
        {
            _btnBack.onClick.AddListener(ClickBack);
        }

        private void OnDisable()
        {
            _btnBack.onClick.RemoveAllListeners();
        }

        private void ClickBack()
        {
            if (_snapShoot.GetViewCount() > 1)
            {
                RefreshView(_snapShoot.BackView());
            }
            else
            {
                UIManager.Instance.BackView();
            }
        }

        protected void Jump(ViewType viewType)
        {
            _snapShoot.PushView(viewType);
            RefreshView(viewType);
        }
        protected void Replace(ViewType viewType)
        {
            _snapShoot.ReplaceView(viewType);
            RefreshView(viewType);
        }

        public virtual void Open(BaseUISnapShoot snapShoot)
        {
            _snapShoot = snapShoot;
        }
        
        public virtual void Close()
        {
            
        }

        protected virtual void RefreshView(ViewType viewType)
        {
            RefreshTitle();
        }
        
        private void RefreshTitle()
        {
            _textTitle.text = _snapShoot.GetTopView().ToString();
        }

    }
}
