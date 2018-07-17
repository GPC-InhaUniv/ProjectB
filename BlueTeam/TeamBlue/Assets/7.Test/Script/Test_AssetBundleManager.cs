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



    public GameObject PlayerInstancing()
    {
        GameObject player;
        player = Instantiate(playerBundle.LoadAsset(AssetName) as GameObject);
        return player;
    }

}
