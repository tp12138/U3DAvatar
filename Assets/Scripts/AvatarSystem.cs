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
    public Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> skinnedSources;//���п��滻��Դ���ֵ���Ϣ
     [HideInInspector]
    public GameObject target;//��Դʵ��Ŀ��
    [HideInInspector]
    public GameObject sourcesGameObject;//��Դ����
    [HideInInspector]
    public Transform sourceTransform;//��Դ��Դ
    [HideInInspector]
    public Transform[] targetHips;//Ŀ�������Ϣ
    [HideInInspector]
    public string[,] targetDatas=new string[,] { {"eyes","1"},{"hair","1"},{"top","1"},{"pants","1"},{"shoes","1"},{"face","1"}};//Ŀ�����λ��Ӧ���Ǽ��Ų���
    [HideInInspector]
    public Dictionary<string, SkinnedMeshRenderer> targetMesh;//Ŀ������,��ͬ��λ��Ӧ��mesh
    [HideInInspector]
    public AssetBundle ab;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this); //��ɾ����Ϸ����
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
    //��ʼ��
   /* void init()
    {
        
        loadSkinedInfo(sourceTransform,skinnedSources, target, targetMesh);
        initCharacter(targetDatas);
    }*/


    //��ʼ����Դ,����Ǽ���Ϣ
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

    //������滻�ķ�װ����Ϣ,Ŀ���������ɶ�Ӧ��λ
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
                //�����±����ɶ�Ӧ��skm
                GameObject partGo = new GameObject();
                partGo.name = names[0];
                partGo.transform.parent = target.transform;
                smr.Add(names[0], partGo.AddComponent<SkinnedMeshRenderer>()); //�ѹ���target���ϵ�skm��Ϣ�洢����λֻ��¼һ��
                datas.Add(names[0], getDict());
            }
            datas[names[0]].Add(names[1], temp);//����λ,���ö�Ӧ�Ĳ�λ��ź�skined
       }
    }*/
    
    //��ʼ��Ŀ���ɫ�ķ�װ
   /* void initCharacter(string[,] targetStr)
    {
        int length = targetStr.GetLength(0);//�������
        for (int i = 0; i < length; i++)
        {
            changeMesh(targetDatas[i, 0], targetDatas[i, 1], skinnedSources, targetHips, targetMesh, targetStr); //�����·�
        }
   
    }*/

    //������װ
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
    //���浱ǰĿ�����ϵķ�װ��Ϣ
    void saveData(string part, string num, string[,] targetStr)
    {
        int length = targetStr.GetLength(0);//�������
        for (int i = 0; i < length; i++)
        {
            if (targetStr[i, 0] == part)
            {
                targetStr[i, 1] = num;
            }
        }
    }

    //�ⲿ����,����Ŀ���װ
    public void OnChangePeople(string part, string num)
    {
        changeMesh(part, num);
    }

    //��ȡ���п��滻��װ������
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
