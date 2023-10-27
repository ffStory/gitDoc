using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour
{
    void OnEnable()
    {
        
        var codePath = Path.Combine(Directory.GetCurrentDirectory(), "../../Resource/Data/CostConfig.byte");
        using var stream2 = new FileStream(codePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

        var costDic = CostConfigResMapMsg.Parser.ParseFrom(stream2);

        // var costitem = new CostItemMsg();
        // costitem.CostType = CostItemType.Between;
        // costitem.AttrName = "11";
        // var array = costitem.ToByteArray();
        // var msg = CostItemMsg.Parser.ParseFrom(array);
        //
        Debug.Log(costDic);

    }
}
