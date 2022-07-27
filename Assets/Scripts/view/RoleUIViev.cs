using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class RoleUIViev : MonoBehaviour
{
    [HideInInspector]
    public AvatarModel avatarModel;
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
   
    // Start is called before the first frame update
    void Awake()
    {
        targetSkinned = new Dictionary<string, SkinnedMeshRenderer>();
        avatarModel = gameObject.GetComponent<AvatarModel>();
        avatarControl = gameObject.GetComponent<AvatarControl>();
        avatarModel.onUpdatePart += changeMesh;
        avatarModel.onAddNewPart += setNewPart;
        
    }


    /// <summary>
    /// 修改场景中模型的对应部位的蒙皮网格渲染器的属性
    /// </summary>
    /// <param name="part">部位</param>
    /// <param name="num">部位中新蒙皮的编号</param>
    public void changeMesh(string part, string num)
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
    }

    /// <summary>
    /// 在骨架下生成部位
    /// </summary>
    /// <param name="partName">部位名字</param>
    public void setNewPart(string partName)
    {
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
    public void initTargetBones()
    {
        targetHipsDict = new Dictionary<string, Transform>();
        targetHips = target.GetComponentsInChildren<Transform>();
        //保存目标骨架身上从名字到骨骼的映射,直接查找
        foreach (var t in targetHips)
            targetHipsDict.Add(t.name, t);
    }

}
