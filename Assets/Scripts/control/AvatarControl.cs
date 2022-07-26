using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;
public class AvatarControl : MonoBehaviour
{
    [HideInInspector]
    public AvatarModel avatarModel;
    [HideInInspector]
    public RoleUIViev roleUIViev;
    [HideInInspector]
    public static AvatarControl _instance;
    public TextAsset luaScript;
    public void Awake()
    {
         _instance = this;
        DontDestroyOnLoad(this); //不删除游戏物体
        avatarModel = gameObject.GetComponent<AvatarModel>();
        roleUIViev = gameObject.GetComponent<RoleUIViev>();
        avatarModel.init();
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.DoString(luaScript.text);
        initCharacter();
    }


    /// <summary>
    /// 初始化角色的各个部件mesh
    /// </summary>
    public void initCharacter()
    {
        var targetDatas = avatarModel.targetDatas;
        for (int i = 0; i < targetDatas.GetLength(0); i++)
            OnChangePeople(targetDatas[i, 0], targetDatas[i, 1]);
    } 
    
    //外部调用,更改目标服装
    public void OnChangePeople(string part, string num)
    {
        roleUIViev.changeMesh(part, num);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 根据资源名字和类型,加载资源
    /// </summary>
    /// <param name="sourceName">资源名字</param>
    /// <param name="type">资源类型</param>
    /// <returns></returns>
    // [LuaCallCSharp]
    public Sprite loadSpriteFromAssetBundle(string sourceName)
    {
        Texture2D ass = avatarModel.ab.LoadAsset<Texture2D>(sourceName + ".jpg");
        Sprite mySprite = Sprite.Create(ass, new Rect(0.0f, 0.0f, ass.width, ass.height), new Vector2(0.5f, 0.5f), 100.0f);
        return mySprite;
    }

    /// <summary>
    /// 从保存资源的字典中,移除某个部位某个编号的部件
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">编号</param>
    /// <returns></returns>
    public bool removeMesh(GameObject go)
    {
        
        return avatarModel.removeSkinnedMesh(go);
    }
}
