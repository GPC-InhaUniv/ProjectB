using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// http://wergia.tistory.com/36?category=737654
/// </summary>

public class Test_AssetBundleManager : Singleton<Test_AssetBundleManager>
{

    protected Test_AssetBundleManager() { }

    public string AssetName;

    public string path;

    public AssetBundle player;
    private void Start()
    {
        path =Application.persistentDataPath + "/AssetBundles/character.unity3d";
        StartCoroutine(test());
    }

    IEnumerator test()
    {

        player = AssetBundle.LoadFromFile(path);
        if (player == null)
        {
            Debug.Log("Fail");
            yield break;

        }
        else
            Debug.Log("Successe");

       

        /*
       using (WWW www = new WWW(path))
       {
           yield return www;
           AssetBundle bundle = www.assetBundle;
       }

       AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync("test", "TestBundle", typeof(GameObject));

       if (request == null)
           yield break;
       yield return StartCoroutine(request);
       GameObject prefab = request.GetAsset<GameObject>();
       if (prefab != null)
           GameObject.Instantiate(prefab);*/
    }
}
