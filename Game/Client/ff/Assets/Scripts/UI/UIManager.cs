
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public enum UIType
{
    Lottery = 0,
    LotteryDetail = 1,
    
    Hero = 10,
    HeroInfo = 11
}

public class BaseUISnapShoot
{
    public UIType UIType;
}


public class UIManager
{
    private UIManager()
    {
        SnapShoots = new List<BaseUISnapShoot>();
        Views = new List<BaseView>();
    }


    public void OpenUI(BaseUISnapShoot snapShoot)
    {
        var config = new object(); //ui配置文件 路径 等等
        
        switch (snapShoot.UIType)
        {
            case UIType.Lottery:
                
                break;
        }
    }
    
    
    private UIManager _instance;
    public UIManager Instance => _instance ??= new UIManager();

    public List<BaseUISnapShoot> SnapShoots;
    public List<BaseView> Views;
}
