using System.Linq;
using Logic.Manager;
using UnityEngine.UI;

namespace UI.Views
{
    public class HeroListView : BaseView
    {
        public static readonly string Path = "UI/Prefab/Views/HeroListView";
        public static string Name => Path.Split('/').Last();
        public override void Open(BaseUISnapShoot snapShoot)
        {
            base.Open(snapShoot);
            var button = transform.Find("Root/Button").GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate
            {
                UIManager.Instance.JumpView(new BaseUISnapShoot(UIType.HeroInfo){ViewJumpCloseType = ViewCloseType.None, ViewBackCloseType = ViewCloseType.Destroy});
                // Jump(UIType.HeroInfo);
            });
            
            RefreshView();
        }
    }
}
