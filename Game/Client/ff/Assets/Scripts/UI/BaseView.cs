using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    public static string ViewPath = string.Empty;
    public string ViewName = string.Empty;
    public BaseUISnapShoot UISnapShoot;


    public virtual void Open(BaseUISnapShoot SnapShoot)
    {
        UISnapShoot = SnapShoot;
    }
}
