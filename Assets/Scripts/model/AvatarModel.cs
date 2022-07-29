using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;
[LuaCallCSharp]
[Hotfix]
public class AvatarModel : MonoBehaviour
{

   
    [HideInInspector]
    public string[,] targetDatas = new string[6, 2];
    public Action<string,string,string> onUpdatePart;
    public Action<string> onAddNewPart;
    [HideInInspector]
    public LuaTable scriptEnv;
    public TextAsset luaScript;
    [HideInInspector]
    internal static LuaEnv luaEnv = new LuaEnv();
  
    private string state;
    public void Awake()
    {
        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        luaEnv.DoString(luaScript.text, "AvatarModle.Lua", scriptEnv);
    }

    /// <summary>
    /// 更新角色对象身上part部位的资源为num编号资源
    /// </summary>
    /// <param name="part"></param>
    /// <param name="num"></param>
    [Hotfix]
    public void updateData(string part, string num)
    {
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
            configRoleDate(i,part[i],num[i]);       
    }

    [Hotfix]
    public void configRoleDate(int index,string part,string num)
    {
      
    }

}
