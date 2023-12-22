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
                UIManager.Instance.PushView(new BaseUISnapShoot(UIType.HeroInfo){ViewDeActivePushType = ViewDeActiveType.None, ViewDeActiveBackType = ViewDeActiveType.Destroy});
                // Jump(UIType.HeroInfo);
            });
            
            RefreshView();
        }
        
    }
}
