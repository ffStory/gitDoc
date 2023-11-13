using Logic.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Item
{
    public class TopNavigation : MonoBehaviour
    {
        [SerializeField]private Button btnBack;
        [SerializeField]private Text txtTitle;

        private void Start()
        {
            btnBack.onClick.AddListener(OnClickBack);
        }

        private void OnClickBack()
        {
            UIManager.Instance.BackView();
        }

        public void Refresh(UIConfigResMsg config)
        {
            txtTitle.text = config.Title;
        }
    }
}
