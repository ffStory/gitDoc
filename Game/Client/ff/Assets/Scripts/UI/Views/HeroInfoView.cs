using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class HeroInfoView : BaseView
    {
        public static string Name = "HeroInfoView";
        public static string Path = "UI/Prefab/Views/HeroInfoView";
        public static UILayer Layer = UILayer.Top;
        public static ViewCloseType CloseType = ViewCloseType.Destroy;

        public override void Open(BaseUISnapShoot snapShoot)
        {
            base.Open(snapShoot);

            var button = transform.Find("Root/Button").GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate
            {
                // UIManager.Instance.JumpView(new BaseUISnapShoot(HeroListView.Name, ViewType.HeroList));
                Replace(ViewType.HeroList1);
            });
            RefreshView(snapShoot.GetTopView());
        }
    }
}
