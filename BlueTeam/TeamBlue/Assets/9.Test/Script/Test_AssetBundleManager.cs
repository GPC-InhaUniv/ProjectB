using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Test_AssetBundleManager : Singleton<Test_AssetBundleManager> {
    
    public string path;
    protected Test_AssetBundleManager() { }

    public string AssetBundleName;
    public string AssetName;
    
    AssetBundle playerBundle;
    
    AssetBundle publicBundle;
    
    AssetBundle nextSceneBundle;

    private void Awake()
    {

        path = System.IO.Path.Combine( Application.persistentDataPath,"/6.AssetBundles/");//"file://" + Application.dataPath + "/6.AssetBundles/character";
    }
    private void Start()
    {
        
        StartCoroutine(PlayerBundleSeting());
    }


    IEnumerator PlayerBundleSeting()
    {
        WWW www = new WWW(path+ "character" + ".unity3d");
            yield return www;
            playerBundle = www.assetBundle;

    }

    IEnumerator PublicBundleSetting()
    {
        using (WWW www = new WWW(path + "public"+ ".unity3d"))
        {
            yield return www;
            publicBundle = www.assetBundle;
        }
    }

    public void LoadAsset()
    {
        StartCoroutine(LoadBundle());
    }

    IEnumerator LoadBundle()
    {
        if(nextSceneBundle != null)
        {
            nextSceneBundle.Unload(true);
            nextSceneBundle = null;
        }
        using (WWW www = new WWW(path + AssetBundleName + ".unity3d"))
        {
            yield return www;
            nextSceneBundle = www.assetBundle;   
        }
    }

}
