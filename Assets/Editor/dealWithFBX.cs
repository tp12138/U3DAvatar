using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public static class dealWithFBX
{

  // public GameObject[] fbx;
  // public GameObject target;
   public static void dealMesh(GameObject source,GameObject target)
    {
       SkinnedMeshRenderer[] smrs = source.GetComponentsInChildren<SkinnedMeshRenderer>();
       Dictionary<string, SkinnedMeshRenderer> sourceSmrs = new Dictionary<string, SkinnedMeshRenderer>();
       //保存原模型的名字到skinnedMesh字典
       foreach (var r in smrs)
         sourceSmrs.Add(r.name, r);
       Dictionary<string, Transform> nameToBones = new Dictionary<string, Transform>();//骨架中,名字到骨骼的对应
       Transform[] trans = target.GetComponentsInChildren<Transform>();//获取骨架中的骨骼资源
       Dictionary<string, Mesh> nameToMesh = new Dictionary<string, Mesh>();//剥离出来的Mesh,名字到Mesh的字典
       //保存骨架中名字到骨骼的映射
       for (int i = 0; i < trans.Length; i++)
           nameToBones.Add(trans[i].name, trans[i]);
       //从原模型中剥离出mesh并保存名字到新Mesh的映射
       for (int i = 0; i < smrs.Length; i++)
       {
           Mesh tempMesh = smrs[i].sharedMesh;
           Mesh newMesh = UnityEngine.Object.Instantiate<Mesh>(tempMesh);
           newMesh.name = string.Format(tempMesh.name);
           string newMeshPath = Path.Combine("Assets/Asset/", string.Format("{0}.{1}", newMesh.name, "asset"));
           AssetDatabase.CreateAsset(newMesh, newMeshPath);
           AssetDatabase.SaveAssets();
           AssetDatabase.Refresh();
           Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(newMeshPath);
           if (mesh != null) nameToMesh.Add(mesh.name, mesh);
           else Debug.Log("can`t load mesh:" + newMeshPath);
           //Object.DestroyImmediate(newMesh);
       }

       var materials = Resources.LoadAll<Material>("");
       int num = 0;
       //根据材质球,不断生成新的skinnedMesh 并且保存预制体
       if(materials!=null&&materials.Length!=0){
           string nameOld = materials[0].name.Split('_')[0].Split('-')[0];
           for (int i = 0; i < materials.Length; i++)
           {
               string[] names = materials[i].name.Split('_');
               string[] tnames = names[0].Split('-');
               if (nameOld.Equals(tnames[0]))
               {
                   num++;
               }
               else
               {
                   num = 1;
                   nameOld = tnames[0];
               }
               GameObject go = new GameObject();
               SkinnedMeshRenderer newSmr = go.AddComponent<SkinnedMeshRenderer>();
               SkinnedMeshRenderer oldSmr=sourceSmrs[names[0]];
               List<Transform> bones = new List<Transform>();
               foreach (var t in oldSmr.bones)
               {
                   if(nameToBones.ContainsKey(t.name))
                       bones.Add(nameToBones[t.name]);
               }
               newSmr.bones = bones.ToArray();//重新配置骨骼
               newSmr.sharedMesh = nameToMesh[names[0]];//重新配置mesh
               newSmr.material = materials[i];
               newSmr.rootBone = nameToBones[sourceSmrs[names[0]].rootBone.name];
               go.name = tnames[0] + "-" + num.ToString();
               newSmr.localBounds = sourceSmrs[names[0]].bounds;
               //Debug.Log(PrefabUtility.CreatePrefab(Application.dataPath + "/Prefabs/" + go.name + ".prefab", go)==null);
               PrefabUtility.CreatePrefab(Application.dataPath + "/Prefabs/" + go.name + ".prefab", go);
               Object.DestroyImmediate(go);
           }
       }
       
    }

   public static void dealAnimationClip(GameObject[] fbx,GameObject target)
   {
       Dictionary<string,AnimationClip> list = new Dictionary<string,AnimationClip>();
       for (int i = 0; i < fbx.Length;i++ )
       {
           var t = fbx[i];
           var anim = t.GetComponent<Animation>().clip;
           AnimationClip newClip = new AnimationClip();
           EditorUtility.CopySerialized(anim, newClip);
           newClip.name = string.Format(anim.name);
           string newAnimClpiPath = Path.Combine("Assets/Asset/", string.Format("{0}.{1}", newClip.name, "anim"));
           AssetDatabase.CreateAsset(newClip, newAnimClpiPath);
           AssetDatabase.SaveAssets();
           if(list.ContainsKey(anim.name)==false)
                list.Add(anim.name, AssetDatabase.LoadAssetAtPath<AnimationClip>(newAnimClpiPath));
       }
       Animation animation;
       if(target.GetComponent<Animation>()!=null)
          animation=target.GetComponent<Animation>();
       else animation = target.AddComponent<Animation>();

       foreach(KeyValuePair<string,AnimationClip> t in list){
           animation.AddClip(t.Value,t.Key);
       }
       animation.clip = list["idle1"]; 
   }
   [MenuItem("Assets/Create/Shader/BuildPrefabFromFbx",false,208)]
   public static void CreatPartPrefab()
   {
       var sel = Selection.GetFiltered<UnityEngine.GameObject>(SelectionMode.DeepAssets);
       GameObject[] fbx = new GameObject[sel.Length - 1];
       List<GameObject> fbxList = new List<GameObject>();
       List<GameObject> targetList = new List<GameObject>();
       List<GameObject> defaultFbxList = new List<GameObject>();
       for (int i = 0; i < sel.Length; i++)
       {
           if (sel[i].name.Equals("target"))
               targetList.Add(sel[i]);
           else if (sel[i].name.Equals("Female"))
               defaultFbxList.Add(sel[i]);
           else {
               fbxList.Add(sel[i]);
           } 
       }
       dealAnimationClip(fbxList.ToArray(), targetList[0]);
       dealMesh(defaultFbxList[0], targetList[0]);
       
   }
}
