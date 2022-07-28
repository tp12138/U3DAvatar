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
    [HideInInspector]
    public AssetBundle ab;    //包含骨架的AB包
    [HideInInspector]
    private AssetBundle prefabAB; //包含替换资源和UI贴图的AB包
    [HideInInspector]
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSourceDict;//可替换资源的部位->编号->蒙皮字典

    public TextAsset luaScript;//Lua文件资源
    public void Awake()
    {
         _instance = this;
        DontDestroyOnLoad(this); //不删除游戏物体
        avatarModel = gameObject.GetComponent<AvatarModel>();
        roleUIViev = gameObject.GetComponent<RoleUIViev>();
        skinnedSourceDict = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.DoString(luaScript.text);
        initCharacter();
    }

    /// <summary>
    /// 将AB包中的替换部位资源保存进字典中
    /// </summary>
    /// <param name="prefabName"></param>
    [LuaCallCSharp]
    public void save(string prefabName)
    {
        skinnedSourceDict.Clear();

        SkinnedMeshRenderer[] res =loadAllSourcesFromAssetBundle(prefabName);
        foreach (var t in res)
        {
            string[] names = t.name.Split('-');
            if (skinnedSourceDict.ContainsKey(names[0]) == false)//初次在骨骼target下生成对应的身体组件
            {
                skinnedSourceDict.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>());
            }
            skinnedSourceDict[names[0]].Add(names[1], t);
        }
    }
    /// <summary>
    /// 初始化角色mesh
    /// </summary>
    public void initCharacter()
    {
        string[,] targetDatas = avatarModel.targetDatas;
        for (int i = 0; i < targetDatas.GetLength(0); i++)
            OnChangePeople(targetDatas[i, 0], targetDatas[i, 1]);
    }


    //外部调用,更改目标服装
    public void OnChangePeople(string part, string num)
    {
        avatarModel.updateData(part, num);
        switch (part)
        {
            case "pants":
                PlayAnimation("item_pants");
                break;
            case "shoes":
                PlayAnimation("item_boots");
                break;
            case "top":
                PlayAnimation("item_shirt");
                break;
            default:
                PlayAnimation("walk");
                break;
        }
       
    }
    /// <summary>
    /// 从保存资源的字典中,移除某个部位某个编号的部件
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">编号</param>
    /// <returns></returns>
    public void removeMesh(GameObject go)
    {
        //avatarModel.removeSkinnedMesh(go);
        string[] names=go.name.Split('-');
        skinnedSourceDict[names[0]].Remove(names[1]);
    }
    
    
    
    /// <summary>
    /// 获取对应部位编号为num的蒙皮网格资源
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">编号</param>
    /// <returns></returns>
    public SkinnedMeshRenderer getSkinnedMeshByPartAndNum(string part, string num)
    {
        if (skinnedSourceDict.ContainsKey(part) == false)
        {
            Debug.LogError("part name error " + part);
            return null;
        }
        else
        {
            if (skinnedSourceDict[part].ContainsKey(num) == false)
            {
                Debug.LogError("the part :" + part + ",do not have num:" + num);
                return null;
            }
        }
        return skinnedSourceDict[part][num];
    }
    
    /// <summary>
    /// 根据资源名字和类型,加载资源
    /// </summary>
    /// <param name="sourceName">资源名字</param>
    /// <param name="type">资源类型</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public Sprite loadSpriteFromAssetBundle(string sourceName)
    {
        Texture2D ass = ab.LoadAsset<Texture2D>(sourceName + ".jpg");
        Sprite mySprite = Sprite.Create(ass, new Rect(0.0f, 0.0f, ass.width, ass.height), new Vector2(0.5f, 0.5f), 100.0f);
        return mySprite;
    }

    
    /// <summary>
    /// 从骨骼预制体资源包中获取名字对应的骨架
    /// </summary>
    /// <param name="sourceName">骨架名字</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public System.Object loadSourcesFromAssetBundle(string sourceName)
    {
        var obj = ab.LoadAsset(sourceName);
        return obj;
    }

    /// <summary>
    /// 获取预制体资源包中所有的可替换部件资源
    /// </summary>
    /// <param name="AssetBundleName">资源包的包名</param>
    /// <returns></returns>
    public SkinnedMeshRenderer[] loadAllSourcesFromAssetBundle(string AssetBundleName)
    {
        if (prefabAB == null)
            prefabAB = AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + AssetBundleName);

        UnityEngine.Object[] obj = prefabAB.LoadAllAssets();
        List<SkinnedMeshRenderer> sourceSkinnedMesh = new List<SkinnedMeshRenderer>();
        for (int i = 0; i < obj.Length; i++)
        {
            GameObject t = obj[i] as GameObject;
            if (t.GetComponent<SkinnedMeshRenderer>() != null)
            {
                sourceSkinnedMesh.Add(t.GetComponent<SkinnedMeshRenderer>());
            }
        }
        return sourceSkinnedMesh.ToArray();
    }

    //加载目标资源包
    public AssetBundle LoadAssetBundle(string bundleName)
    {
        return AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + bundleName);
    }

    //转Object类型为GameObject类型
    public GameObject TypeChange(UnityEngine.Object go) { return (GameObject)go; }

    //获取所有可替换服装的名字
    public List<string> getAllMeshNameOfPart(string partName)
    {
        List<string> temp = new List<string>();
        foreach (KeyValuePair<string, SkinnedMeshRenderer> t in skinnedSourceDict[partName])
        {
            temp.Add(t.Value.name);
        }
        return temp;
    }

    public void PlayAnimation(string animName)
    { //换装动画名称

        Animation anim = GameObject.FindWithTag("Player").GetComponent<Animation>();
        if (!anim.IsPlaying(animName))
        {
            anim.Play(animName);
            anim.PlayQueued("idle1");
        }

    }
}
