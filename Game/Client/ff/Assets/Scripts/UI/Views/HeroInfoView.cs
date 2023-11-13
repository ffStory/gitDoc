using System.Linq;
using Logic.Manager;
using UnityEngine.UI;

namespace UI.Views
{
    public class HeroInfoView : BaseView
    {
        public static string Path = "UI/Prefab/Views/HeroInfoView";
        public static string Name => Path.Split('/').Last();
        public override void Open(BaseUISnapShoot snapShoot)
        {
            base.Open(snapShoot);

            var button = transform.Find("Root/Button").GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate
            {
                UIManager.Instance.JumpView(new BaseUISnapShoot(UIType.HeroList){ViewJumpCloseType = ViewCloseType.Hide, ViewBackCloseType = ViewCloseType.Hide});
                // Replace(ViewType.HeroList1);
            });
            RefreshView();
        }
    }
}
