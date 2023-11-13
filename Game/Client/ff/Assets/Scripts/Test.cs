using Logic.Manager;
using UnityEngine;

public class Test : MonoBehaviour
{
    void OnEnable()
    {
        var game = new Game();
        game.Init();
        
        
        UIManager.Instance.JumpView(new BaseUISnapShoot(UIType.HeroList));
    }
}
