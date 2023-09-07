using UnityEngine.UI;

namespace UI.Views
{
    public class HeroListView : BaseView
    {
        public static string Name = "HeroListView";
        public static string Path = "UI/Prefab/Views/HeroListView";
        public static UILayer Layer = UILayer.Middle;
        public static ViewCloseType CloseType = ViewCloseType.Disable;


        public override void Open(BaseUISnapShoot snapShoot)
        {
            base.Open(snapShoot);
            var button = transform.Find("Root/Button").GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate
            {
                UIManager.Instance.JumpView(new BaseUISnapShoot(HeroInfoView.Name, ViewType.HeroInfo));
            });
            
            RefreshView(snapShoot.GetTopView());
        }
    }
}
