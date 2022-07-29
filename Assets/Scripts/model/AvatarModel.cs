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
    /// ���½�ɫ��������part��λ����ԴΪnum�����Դ
    /// </summary>
    /// <param name="part"></param>
    /// <param name="num"></param>
    [Hotfix]
    public void updateData(string part, string num)
    {
    }

    /// <summary>
    /// ��ʼ����ɫ���ϵĲ�����Ϣ
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
