using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public static class AssetBundleManage
{
    [MenuItem("AssetBundle/build")]
    public static void Build_WINDOWS64()
    {
        string outputPath = Application.dataPath + "/AssetBundle";
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        BuildPipeline.BuildAssetBundles(outputPath, 0, EditorUserBuildSettings.activeBuildTarget);//自己选择平台
        AssetDatabase.Refresh();
       // EditorUtility.ClearProgressBar();
    }
    


}
