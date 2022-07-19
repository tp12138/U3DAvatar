using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
[LuaCallCSharp]
public class AvatarSystem : MonoBehaviour
{
    [HideInInspector]
    public static AvatarSystem _instance;
    [HideInInspector]
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSources;//所有可替换资源的字典信息
    [HideInInspector]
    public GameObject target;//资源实例目标
    [HideInInspector]
    public Transform[] targetHips;//目标骨骼信息
    [HideInInspector]
    public string[,] targetDatas = new string[6, 2]; //{ {"eyes","1"},{"hair","1"},{"top","1"},{"pants","1"},{"shoes","1"},{"face","1"}};//目标各部位对应的是几号材质
    [HideInInspector]
    public Dictionary<string, SkinnedMeshRenderer> targetMesh;//目标身上,不同部位对应的mesh
    [HideInInspector]
    public AssetBundle ab;
    [HideInInspector]
    public AssetBundle prefabsAb;
    [HideInInspector]
    Dictionary<String,Transform> targetHipsDict;
    [HideInInspector]
    public List<SkinnedMeshRenderer> sourceSKinnedMesh;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this); //不删除游戏物体
        skinnedSources = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
        targetMesh = new Dictionary<string, SkinnedMeshRenderer>();
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.AddLoader(LoadLuaScript);
        luaEnv.DoString("require'AvatarSystem'");
        initCharacter();

    }
    void Start()
    {    
    }

    // Update is called once per frame
    void Update()
    {
    }
    public byte[] LoadLuaScript(ref string filename)
    {
        string path = Application.dataPath + "/Scripts/" + filename + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(path));
   
    }

    public void initCharacter()
    {
        for (int i = 0; i < targetDatas.GetLength(0);i++ )
            OnChangePeople(targetDatas[i,0],targetDatas[i,1]);
    }

    [LuaCallCSharp]
    public void save(string prefabName)
    {
        skinnedSources.Clear();
        targetMesh.Clear();
        SkinnedMeshRenderer[] res = this.loadAllSourcesFromAssetBundle(prefabName);
        //Debug.Log(res.Length);
        foreach (var t in res)
        {
            string[] names = t.name.Split('-');
            if (skinnedSources.ContainsKey(names[0]) == false)//初次在骨骼target下生成对应的身体组件
            {
                GameObject go = new GameObject();
                go.name = names[0];
                go.transform.parent = target.GetComponent<Transform>();
                targetMesh.Add(names[0], go.AddComponent<SkinnedMeshRenderer>());
                skinnedSources.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>());
            }
            skinnedSources[names[0]].Add(names[1], t);
        }
        targetHipsDict = new Dictionary<string, Transform>();
        foreach (var t in targetHips)
            targetHipsDict.Add(t.name, t);
            
    }

    //更换服装
     void changeMesh(string part, string num)
    {    
        SkinnedMeshRenderer smrTemp = skinnedSources[part][num];
        List<Transform> bones = new List<Transform>();
        foreach (var trans in smrTemp.bones)
           if (targetHipsDict.ContainsKey(trans.name))
               bones.Add(targetHipsDict[trans.name]);

        targetMesh[part].material = smrTemp.sharedMaterial;
        targetMesh[part].bones = bones.ToArray();
        targetMesh[part].sharedMesh = smrTemp.sharedMesh;
        targetMesh[part].rootBone = smrTemp.rootBone;
        saveData(part, num, targetDatas);
    }
    //保存当前目标身上的服装信息
    void saveData(string part, string num, string[,] targetStr)
    {
        int length = targetStr.GetLength(0);//获得行数
        for (int i = 0; i < length; i++)
        {
            if(targetDatas[i,0].Equals(part))
            {
                targetDatas[i, 1] = num;
                break;
            }
        }
    }

    [LuaCallCSharp]
    public void configCharater(string[] part, string[] num)
    {
        int length=targetDatas.GetLength(0);
       // Debug.Log(part.Length);
        for (int i = 0; i < length; i++)
            {
                targetDatas[i, 0] = part[i];
                targetDatas[i, 1] = num[i];
            }
    }

    //外部调用,更改目标服装
    public void OnChangePeople(string part, string num)
    {
        changeMesh(part, num);
    }

    //获取所有可替换服装的名字
    public List<string> getAllMeshNameOfPart(string partName)
    {
        List<string> temp = new List<string>();
        SkinnedMeshRenderer[] skinnedMeshs = this.loadAllSourcesFromAssetBundle("prefabs.u3d"); //this.sourceTransform.GetComponentsInChildren<SkinnedMeshRenderer>();
       foreach (var mesh in skinnedMeshs)
        {
           string[] str = mesh.name.Split('-');
           if(str[0].Equals(partName))
             temp.Add(mesh.name);   
        }
        return temp;
    }
    [LuaCallCSharp]
    public AssetBundle LoadAssetBundle(string bundleName)
    {
        return AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + bundleName);
    }
    [LuaCallCSharp]
    public System.Object loadSourcesFromAssetBundle(string sourceName,Type type)
    {
        var obj = ab.LoadAsset(sourceName,type);
        return obj;
    }
    [LuaCallCSharp]
    public System.Object loadSourcesFromAssetBundle(string sourceName)
    {
        var obj = ab.LoadAsset(sourceName);
        return obj;
    }



    public SkinnedMeshRenderer[] loadAllSourcesFromAssetBundle(string AssetBundleName)
    {
        if(prefabsAb==null)
            prefabsAb = AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + AssetBundleName);
       
        UnityEngine.Object[] obj = prefabsAb.LoadAllAssets();
        sourceSKinnedMesh = new List<SkinnedMeshRenderer>();
       for (int i = 0; i < obj.Length; i++)
        {
            GameObject t=obj[i] as GameObject;
            if (t.GetComponent<SkinnedMeshRenderer>() != null)
            {
                sourceSKinnedMesh.Add(t.GetComponent<SkinnedMeshRenderer>());
            }
        }
        return sourceSKinnedMesh.ToArray();
    }
    public GameObject TypeChange(object _object) { return (GameObject)_object; }
    public Sprite TypeChangeToSprite(System.Object _object) { return (Sprite)_object; }
    [LuaCallCSharp]
    public Dictionary<string, SkinnedMeshRenderer> getDict()
    {
        return new Dictionary<string, SkinnedMeshRenderer>();
    }
    [LuaCallCSharp]
    public Transform[] getBonesFromObj(GameObject tar)
    {
        return tar.GetComponentsInChildren<Transform>();
    }


}
