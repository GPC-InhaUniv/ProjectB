using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoadAssetBundleExample : MonoBehaviour
{

    private string BundleURL;

    // 번들의 version 
    public int version;
    void Start()
    {
#if UNITY_ANDROID
  //  BundleURL = "https://docs.google.com/uc?export=download&id=18ic7M3z4M1XFhPGZ4BndeoUgONdQ8GZg";
        BundleURL = "https://docs.google.com/uc?export=download&id=10KRqu8GtuwEi-ILY9pdlMM3Ppi4vDBkY";
#else
    BundleURL = "https://docs.google.com/uc?export=download&id=1faKphTAPWBpx3YovaPE9fVvtEdO2psFW";
#endif
    }
    IEnumerator LoadAssetBundle_Android()
    {
        string uri = BundleURL;
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri, 0);
        yield return request.Send();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        for (int i = 0; i < 3; i++)
        {
            AssetBundleRequest bundleRequest = bundle.LoadAssetAsync("Cube " + (i + 1),
                typeof(GameObject));
            yield return request;
            GameObject obj = Instantiate(bundleRequest.asset) as GameObject;
            obj.transform.position = new Vector3(-4 + (i * 5), 0.0f, 0.0f);
            //       t[i].text = i + " 로드 완료";
        }

    }

    IEnumerator LoadAssetBundle()
    {
        while (!Caching.ready)
            yield return null;
        using (WWW www = WWW.LoadFromCacheOrDownload(BundleURL, version))
        {
            yield return www;
            if (www.error != null)
                throw new Exception("WWW 다운로드에 에러가 생겼습니다.:" + www.error);
            AssetBundle bundle = www.assetBundle;

            for (int i = 0; i < 3; i++)
            {
                AssetBundleRequest request = bundle.LoadAssetAsync("Cube " + (i + 1),
                    typeof(GameObject));
                yield return request;
                GameObject obj = Instantiate(request.asset) as GameObject;
                obj.transform.position = new Vector3(-4 + (i * 5), 0.0f, 0.0f);
                //       t[i].text = i + " 로드 완료";
            }

            bundle.Unload(false);
            www.Dispose();
        } // using문은 File 및 Font 처럼 컴퓨터 에서 관리되는 리소스들을 쓰고 나서 쉽게 자원을 되돌려줄수 있도록 기능을 제공 
    }

    IEnumerator TestLoadAssetBundle_Android()
    {
        string uri = BundleURL;
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri, 0);
        yield return request.Send();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        AssetBundleRequest bundleRequest = bundle.LoadAssetAsync("Riko",
            typeof(GameObject));
        yield return request;
        GameObject obj = Instantiate(bundleRequest.asset) as GameObject;
        obj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

 
    }


    public void LoginBtn()
    {
        AccountInfo.Login("12341234", "123123");
#if UNITY_ANDROID

        StartCoroutine(TestLoadAssetBundle_Android());
        DebugClass.SaveAndPrintDebugLog(this, "PlayerID:"+ 123123); 
#else
        StartCoroutine(LoadAssetBundle());
#endif

    }

}
