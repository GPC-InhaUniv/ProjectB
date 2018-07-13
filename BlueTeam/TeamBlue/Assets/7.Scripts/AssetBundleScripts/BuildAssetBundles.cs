using System.Collections;
using System.Collections.Generic;


using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class BuildAssetBundles : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Bundles/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {

        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSXUniversal);

     //   BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
#endif

}
