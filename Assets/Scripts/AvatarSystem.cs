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
    public GameObject sourcesGameObject;//资源物体
    [HideInInspector]
    public Transform sourceTransform;//资源来源
    [HideInInspector]
    public Transform[] targetHips;//目标骨骼信息
    [HideInInspector]
    public string[,] targetDatas=new string[,] { {"eyes","1"},{"hair","1"},{"top","1"},{"pants","1"},{"shoes","1"},{"face","1"}};//目标各部位对应的是几号材质
    [HideInInspector]
    public Dictionary<string, SkinnedMeshRenderer> targetMesh;//目标身上,不同部位对应的mesh
    [HideInInspector]
    public AssetBundle ab;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this); //不删除游戏物体
        skinnedSources = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
        targetMesh = new Dictionary<string, SkinnedMeshRenderer>();
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.AddLoader(LoadLuaScript);
        luaEnv.DoString("require'AvatarSystem'");
        //Debug.Log(target.transform.position);

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
    //初始化
   /* void init()
    {
        
        loadSkinedInfo(sourceTransform,skinnedSources, target, targetMesh);
        initCharacter(targetDatas);
    }*/


    //初始化资源,保存骨架信息
   /* void initSources()
    {
        var temp=loadSourcesFromAssetBundle("FemaleModel Variant") as GameObject;
        sourcesGameObject = Instantiate(temp) as GameObject;
        sourceTransform = sourcesGameObject.GetComponent<Transform>();
        sourcesGameObject.SetActive(false);
        target = GameObject.Instantiate(loadSourcesFromAssetBundle("target")) as GameObject;
        targetHips = target.GetComponentsInChildren<Transform>();
        //var t=target.getc
    }*/

    //保存可替换的服装的信息,目标物体生成对应部位
   /* void loadSkinedInfo(Transform sourceTran,Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> datas, GameObject target, Dictionary<string, SkinnedMeshRenderer> smr)
    {
        if (!sourceTransform) return;
        datas.Clear();
        smr.Clear();
       SkinnedMeshRenderer[] skinnedMeshs = sourceTran.GetComponentsInChildren<SkinnedMeshRenderer>();
       foreach (var temp in skinnedMeshs)
        {
            string[] names = temp.name.Split('-');
            if (!datas.ContainsKey(names[0]))
            {
                //骨骼下边生成对应的skm
                GameObject partGo = new GameObject();
                partGo.name = names[0];
                partGo.transform.parent = target.transform;
                smr.Add(names[0], partGo.AddComponent<SkinnedMeshRenderer>()); //把骨骼target身上的skm信息存储，部位只记录一次
                datas.Add(names[0], getDict());
            }
            datas[names[0]].Add(names[1], temp);//给部位,设置对应的部位编号和skined
       }
    }*/
    
    //初始化目标角色的服装
   /* void initCharacter(string[,] targetStr)
    {
        int length = targetStr.GetLength(0);//获得行数
        for (int i = 0; i < length; i++)
        {
            changeMesh(targetDatas[i, 0], targetDatas[i, 1], skinnedSources, targetHips, targetMesh, targetStr); //穿上衣服
        }
   
    }*/

    //更换服装
     void changeMesh(string part, string num)
    {    
        SkinnedMeshRenderer smrTemp = skinnedSources[part][num];
        List<Transform> bones = new List<Transform>();
        //Debug.Log(targetHips.Length);
        foreach (var trans in smrTemp.bones)
        {
            foreach (var bone in targetHips)
            {
                if (bone.name.Equals(trans.name))
                {
                    bones.Add(bone);
                    break;
                }
            }
        }
        //Debug.Log(targetMesh[part].bounds);
        targetMesh[part].material = smrTemp.material;
        targetMesh[part].bones = bones.ToArray();
        targetMesh[part].sharedMesh = smrTemp.sharedMesh;
        targetMesh[part].rootBone = smrTemp.rootBone;
        //Debug.Log(target.GetComponent<Transform>().position);
        saveData(part, num, targetDatas);
    }
    //保存当前目标身上的服装信息
    void saveData(string part, string num, string[,] targetStr)
    {
        int length = targetStr.GetLength(0);//获得行数
        for (int i = 0; i < length; i++)
        {
            if (targetStr[i, 0] == part)
            {
                targetStr[i, 1] = num;
            }
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
       SkinnedMeshRenderer[] skinnedMeshs = this.sourceTransform.GetComponentsInChildren<SkinnedMeshRenderer>();
       foreach (var mesh in skinnedMeshs)
        {
            if (!mesh.name.Equals("face-1")) {
                string[] str = mesh.name.Split('-');
                if(str[0].Equals(partName))
                    temp.Add(mesh.name);
            }
                
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
