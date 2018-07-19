using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Test_AssetBundleManager : Singleton<Test_AssetBundleManager> {

    protected Test_AssetBundleManager() { }
    
    public string AssetName;

    AssetBundle playerBundle;
    AssetBundle publicBundle;
    AssetBundle nextSceneBundle;

    public string path;

    private void Start()
    {
        path = "file://" + Application.persistentDataPath + "/AssetBundle";
         
    }

    IEnumerator LoadPlayerBundle()
    {
        using (WWW www = new WWW(path))
        {
            yield return www;
        }
    }



}
