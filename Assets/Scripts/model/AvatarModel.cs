using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;
[LuaCallCSharp ]
public class AvatarModel : MonoBehaviour
{

   
    [HideInInspector]
    public string[,] targetDatas = new string[6, 2];
    public Action<string,string> onUpdatePart;
    public Action<string> onAddNewPart;
    

    /// <summary>
    /// 更新角色对象身上part部位的资源为num编号资源
    /// </summary>
    /// <param name="part"></param>
    /// <param name="num"></param>
    public void updateData(string part, string num)
    {
        int length = targetDatas.GetLength(0);//获得行数
        for (int i = 0; i < length; i++)
        {
            if (targetDatas[i, 0].Equals(part))
            {
                targetDatas[i, 1] = num;
                onUpdatePart(part, num);
                break;
            }
        }

    }

    /// <summary>
    /// 初始化角色身上的部件信息
    /// </summary>
    /// <param name="part"></param>
    /// <param name="num"></param>
    public void configCharater(string[] part, string[] num)
    {
        int length = targetDatas.GetLength(0);

        for (int i = 0; i < length; i++)
        {
            targetDatas[i, 0] = part[i];
            targetDatas[i, 1] = num[i];
            onAddNewPart(part[i]);
        }

    }

}
