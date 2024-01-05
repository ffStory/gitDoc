using Logic.Manager;
using UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Item
{
    public class TopNavigation : MonoBehaviour
    {
        [SerializeField]private Button btnBack;
        [SerializeField]private Text txtTitle;
        private BaseView _view;

        public void Init(BaseView view)
        {
            _view = view;
        }
        
        private void Start()
        {
            btnBack.onClick.AddListener(OnClickBack);
        }

        private void OnClickBack()
        {
            UIManager.Instance.BackUI(_view.GetViewBackData());
        }

        public void Refresh(UIConfigResMsg config)
        {
            txtTitle.text = config.Title;
            btnBack.gameObject.SetActive(!UIManager.Instance.CheckLastUI());
        }
    }
}
