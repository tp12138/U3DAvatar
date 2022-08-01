using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;
public class RoleUIViev : MonoBehaviour
{

    [HideInInspector]
    public AvatarControl avatarControl;
    [HideInInspector]
    public GameObject target;//目标骨架
    [HideInInspector]
    public Transform[] targetHips;//目标骨架的骨骼信息
    [HideInInspector]
    public Dictionary<string, SkinnedMeshRenderer> targetSkinned;//目标骨架上部位->蒙皮的信息的映射字典
    [HideInInspector]
    public Dictionary<string, Transform> targetHipsDict;//目标骨架身上 骨骼名字到骨骼的映射
    private LuaEnv luaEnv;
    public TextAsset luaScript;
    [CSharpCallLua]
    delegate void RemoveMesh(GameObject go);
    RemoveMesh a;
    //delegate void removeMesh(GameObject go)
    // Start is called before the first frame update
    void Start()
    {
        targetSkinned = new Dictionary<string, SkinnedMeshRenderer>();
        avatarControl = gameObject.GetComponent<AvatarControl>();
        luaEnv = new LuaEnv();
        luaEnv.DoString(luaScript.text);
        AvatarControl._instance.remove = luaEnv.Global.Get<Action<GameObject>>("OnremoveMesh");
        AvatarControl._instance.tryChangePeople = luaEnv.Global.Get<Action<string, string>>("tryToChangePeople");
      
    }

    public void removeMesh(GameObject go)
    {
        a(go);
    } 

    public byte[] LoadLuaScript(ref string filename)
    {

        string path = Application.dataPath + "/Scripts/control/" + filename + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(System.IO.File.ReadAllText(path));
    }

    public void onInitNewRole(GameObject target)
    {
        this.target = target;// Instantiate(target);
        initTargetBones(this.target);
    }
    /// <summary>
    /// 修改场景中模型的对应部位的蒙皮网格渲染器的属性
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">部位中新蒙皮的编号</param>
    public void changeMesh(string state,string part, string num)
    {
         SkinnedMeshRenderer smrTemp = avatarControl.getSkinnedMeshByPartAndNum(part, num);
         if (smrTemp == null)
            {
                Debug.LogError("not config skinnedMesh");
                return;
            }
         List<Transform> bones =getBonesBySmr(smrTemp);
         //替换SkinnedMesh
         targetSkinned[part].material = smrTemp.sharedMaterial;
         targetSkinned[part].bones = bones.ToArray();
         targetSkinned[part].sharedMesh = smrTemp.sharedMesh;
         targetSkinned[part].rootBone = smrTemp.rootBone;
         switch (state)
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
    /// 在骨架下生成部位
    /// </summary>
    /// <param name="partName">部位名字</param>
    public void setNewPart(string partName)
    {
        //Debug.Log("set a new part :" + partName);
        GameObject go = new GameObject();
        go.name = partName;
        go.transform.parent =target.GetComponent<Transform>();
        targetSkinned.Add(partName,go.AddComponent<SkinnedMeshRenderer>());
    }

    /// <summary>
    /// 获取蒙皮渲染器中骨骼对应骨架中的骨骼集合
    /// </summary>
    /// <param name="smr"></param>
    /// <returns>蒙皮网格渲染器对应的骨骼集合</returns>
    public List<Transform> getBonesBySmr(SkinnedMeshRenderer smr)
    {
        List<Transform> bones = new List<Transform>();
        foreach (var trans in smr.bones)
            if (targetHipsDict.ContainsKey(trans.name))
                bones.Add(targetHipsDict[trans.name]);
        return bones;
    }

    /// <summary>
    /// 获取目标骨架的骨骼信息,制作骨骼名字->骨骼的映射
    /// </summary>
    private void initTargetBones(GameObject tObj)
    {
        this.target = tObj;
        targetHipsDict = new Dictionary<string, Transform>();
        targetHips = target.GetComponentsInChildren<Transform>();
        //保存目标骨架身上从名字到骨骼的映射,直接查找
        foreach (var t in targetHips)
            targetHipsDict.Add(t.name, t);
    }
    private void PlayAnimation(string animName)
    { //换装动画名称

        Animation anim = GameObject.FindWithTag("Player").GetComponent<Animation>();
        if (!anim.IsPlaying(animName))
        {
            anim.Play(animName);
            anim.PlayQueued("idle1");
        }

    }


}
