using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class Test_AssetBundleManager : MonoBehaviour {

        protected Test_AssetBundleManager() { }

        public string AssetName;
        
      
       
        private void Start()
        {
        StartCoroutine(test());
        }

    IEnumerator test()
    {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync("test", "TestBundle", typeof(GameObject));
        
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
        GameObject prefab = request.GetAsset<GameObject>();
        if (prefab != null)
            GameObject.Instantiate(prefab);
    }




    }
