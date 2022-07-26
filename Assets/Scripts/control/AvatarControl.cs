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
        DontDestroyOnLoad(this); //��ɾ����Ϸ����
        avatarModel = gameObject.GetComponent<AvatarModel>();
        roleUIViev = gameObject.GetComponent<RoleUIViev>();
        avatarModel.init();
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.DoString(luaScript.text);
        initCharacter();
    }


    /// <summary>
    /// ��ʼ����ɫ�ĸ�������mesh
    /// </summary>
    public void initCharacter()
    {
        var targetDatas = avatarModel.targetDatas;
        for (int i = 0; i < targetDatas.GetLength(0); i++)
            OnChangePeople(targetDatas[i, 0], targetDatas[i, 1]);
    } 
    
    //�ⲿ����,����Ŀ���װ
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
    /// ������Դ���ֺ�����,������Դ
    /// </summary>
    /// <param name="sourceName">��Դ����</param>
    /// <param name="type">��Դ����</param>
    /// <returns></returns>
    // [LuaCallCSharp]
    public Sprite loadSpriteFromAssetBundle(string sourceName)
    {
        Texture2D ass = avatarModel.ab.LoadAsset<Texture2D>(sourceName + ".jpg");
        Sprite mySprite = Sprite.Create(ass, new Rect(0.0f, 0.0f, ass.width, ass.height), new Vector2(0.5f, 0.5f), 100.0f);
        return mySprite;
    }

    /// <summary>
    /// �ӱ�����Դ���ֵ���,�Ƴ�ĳ����λĳ����ŵĲ���
    /// </summary>
    /// <param name="part">��λ</param>
    /// <param name="num">���</param>
    /// <returns></returns>
    public bool removeMesh(GameObject go)
    {
        
        return avatarModel.removeSkinnedMesh(go);
    }
}
