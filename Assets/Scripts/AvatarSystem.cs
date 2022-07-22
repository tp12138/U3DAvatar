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
    public Transform[] targetHips;//Ŀ�������Ϣ
    [HideInInspector]
    public string[,] targetDatas = new string[6, 2]; //{ {"eyes","1"},{"hair","1"},{"top","1"},{"pants","1"},{"shoes","1"},{"face","1"}};//Ŀ�����λ��Ӧ���Ǽ��Ų���
    [HideInInspector]
    public Dictionary<string, SkinnedMeshRenderer> targetMesh;//Ŀ������,��ͬ��λ��Ӧ��mesh
    [HideInInspector]
    public AssetBundle ab;
    [HideInInspector]
    public AssetBundle prefabsAb;
    [HideInInspector]
    Dictionary<String,Transform> targetHipsDict;
    [HideInInspector]
    public List<SkinnedMeshRenderer> sourceSKinnedMesh;

    /// <summary>
    /// ���ɲ�λ->���->����SkinnedMesh���ֵ�
    /// �������岿λ�����Ӧ��SkinnedMesh���ֵ�
    /// ����lua�ļ���������
    /// ��ʼ��Ŀ���ɫ��Mesh
    /// </summary>
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this); //��ɾ����Ϸ����
        skinnedSources = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
        targetMesh = new Dictionary<string, SkinnedMeshRenderer>();
        LuaEnv luaEnv = new LuaEnv();
        luaEnv.AddLoader(LoadLuaScript);
        luaEnv.DoString("require'AvatarSystem'");
        initCharacter();
        

    }


    /// <summary>
    /// lua�ļ�ת����
    /// </summary>
    /// <param name="filename">Ҫװ�ص�luaģ����</param>
    /// <returns></returns>
    public byte[] LoadLuaScript(ref string filename)
    {
        string path = Application.dataPath + "/Scripts/" + filename + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(path));
   
    }

    /// <summary>
    /// ��ʼ����ɫ�ĸ�������mesh
    /// </summary>
    public void initCharacter()
    {
        for (int i = 0; i < targetDatas.GetLength(0);i++ )
            OnChangePeople(targetDatas[i,0],targetDatas[i,1]);
    }

    /// <summary>
    /// ����Դ�ļ��н�������Դ���浽��Ӧ���ֵ���
    /// ������Դ�еĲ�λ,�ٳ�����Ŀ��Ǽ������ɶ�Ӧ�Ĳ�λ
    /// ���ұ���Ǽ���,�������ֵ�������ӳ��
    /// </summary>
    /// <param name="prefabName">assetBundle����</param>
    [LuaCallCSharp]
    public void save(string prefabName)
    {
        skinnedSources.Clear();
        targetMesh.Clear();
        SkinnedMeshRenderer[] res = this.loadAllSourcesFromAssetBundle(prefabName);
        foreach (var t in res)
        {
            string[] names = t.name.Split('-');
            if (skinnedSources.ContainsKey(names[0]) == false)//�����ڹ���target�����ɶ�Ӧ���������
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
        //����Ŀ��Ǽ����ϴ����ֵ�������ӳ��,ֱ�Ӳ���
        foreach (var t in targetHips)
            targetHipsDict.Add(t.name, t);
            
    }

    /// <summary>
    /// ���ݶ�Ӧ�Ĳ�λ��Ҫ���µ�ģ�ͱ��,�滻�����Ӧ��λ��Mesh
    /// </summary>
    /// <param name="part">��λ</param>
    /// <param name="num">��λ���</param>
     void changeMesh(string part, string num)
    {
        if (skinnedSources.ContainsKey(part) == false)
        {
            Debug.LogError("part name error " + part);
            return;
        }
        else
        {
            if (skinnedSources[part].ContainsKey(num) == false)
            {
                Debug.LogError("the part :" + part + ",do not have num:" + num);
                return;
            }
        }
        SkinnedMeshRenderer smrTemp = skinnedSources[part][num];
        if (smrTemp == null)
        {
            Debug.LogError("not config skinnedMesh");
            return;
        }
        List<Transform> bones = new List<Transform>();
        foreach (var trans in smrTemp.bones)
           if (targetHipsDict.ContainsKey(trans.name))
               bones.Add(targetHipsDict[trans.name]);
         //�滻SkinnedMesh
        targetMesh[part].material = smrTemp.sharedMaterial;
        targetMesh[part].bones = bones.ToArray();
        targetMesh[part].sharedMesh = smrTemp.sharedMesh;
        targetMesh[part].rootBone = smrTemp.rootBone;
        saveData(part, num, targetDatas);
    }
    

    /// <summary>
    /// ��Ŀ����Ϣ����Ķ�ά�����ϱ���,��Ӧ��λ�ı��
    /// </summary>
    /// <param name="part">��λ</param>
    /// <param name="num">���</param>
    /// <param name="targetStr">Ŀ�겿λ��Ϣ����</param>
    void saveData(string part, string num, string[,] targetStr)
    {
        int length = targetStr.GetLength(0);//�������
        for (int i = 0; i < length; i++)
        {
            if(targetDatas[i,0].Equals(part))
            {
                targetDatas[i, 1] = num;
                break;
            }
        }
    }

    /// <summary>
    /// �����������Ĳ�λ����Ͷ�Ӧ��λ�ı���������ó�ʼ�Ľ�ɫ��λ���
    /// </summary>
    /// <param name="part">��λ</param>
    /// <param name="num">���</param>
    [LuaCallCSharp]
    public void configCharater(string[] part, string[] num)
    {
        int length=targetDatas.GetLength(0);

        for (int i = 0; i < length; i++)
            {
                targetDatas[i, 0] = part[i];
                targetDatas[i, 1] = num[i];
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
        foreach (KeyValuePair<string, SkinnedMeshRenderer> t in skinnedSources[partName])
        {
            temp.Add(t.Value.name);
        }
        return temp;
    }
    /// <summary>
    /// ����assetbundle�������ּ���AssetBundle��
    /// </summary>
    /// <param name="bundleName">������</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public AssetBundle LoadAssetBundle(string bundleName)
    {
        return AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundle/" + bundleName);
    }
    /// <summary>
    /// ������Դ���ֺ�����,������Դ
    /// </summary>
    /// <param name="sourceName">��Դ����</param>
    /// <param name="type">��Դ����</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public Sprite loadSpriteFromAssetBundle(string sourceName)
    {
        Texture2D ass = ab.LoadAsset<Texture2D>(sourceName+".jpg");
        Sprite mySprite = Sprite.Create(ass, new Rect(0.0f, 0.0f, ass.width,ass.height), new Vector2(0.5f, 0.5f), 100.0f);
        return mySprite;
    }
    /// <summary>
    /// ������Դ������,��assetbundle���м�����Դ
    /// </summary>
    /// <param name="sourceName">��Դ����</param>
    /// <returns></returns>
    [LuaCallCSharp]
    public System.Object loadSourcesFromAssetBundle(string sourceName)
    {
        var obj = ab.LoadAsset(sourceName);
        return obj;
    }
   /// <summary>
   /// ����Դ���л�ȡ�������е�SkinnedMeshRenderer���
   /// </summary>
   /// <param name="AssetBundleName">��Դ������</param>
   /// <returns></returns>
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
    /// <summary>
    /// ������ת��ΪGameObject����
    /// </summary>
    /// <param name="_object">Ҫ�ı�Ķ���</param>
    /// <returns></returns>
    public GameObject TypeChange(object _object) { return (GameObject)_object; }
    /// <summary>
    /// �����ַ�����SkinnedMeshRenderer���ֵ�
    /// </summary>
    /// <returns></returns>
    [LuaCallCSharp]
    public Dictionary<string, SkinnedMeshRenderer> getDict()
    {
        return new Dictionary<string, SkinnedMeshRenderer>();
    }
   /// <summary>
   /// ��ȡĳ�������µ�ȫ��Transform���
   /// </summary>
   /// <param name="tar"></param>
   /// <returns></returns>
    [LuaCallCSharp]
    public Transform[] getBonesFromObj(GameObject tar)
    {
        return tar.GetComponentsInChildren<Transform>();
    }
    /// <summary>
    /// �ӱ�����Դ���ֵ���,�Ƴ�ĳ����λĳ����ŵĲ���
    /// </summary>
    /// <param name="part">��λ</param>
    /// <param name="num">���</param>
    /// <returns></returns>
    public bool removeMesh(string part, string num)
    {
        bool res=skinnedSources[part].Remove(num);
        return res;
    }
}
