using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadAssetBundleExample : MonoBehaviour
{
    [SerializeField]
     InputField IdInputField;
    [SerializeField]
     InputField PwInputFiled;


     string BundleURL;
     string BundleURL2;
    string BundleURL3;
    string savePath;
    // 번들의 version 
    public int version;

    string assetBundleDirectory;



    void Start()
    {

#if UNITY_ANDROID
  
        BundleURL = "https://docs.google.com/uc?export=download&id=10KRqu8GtuwEi-ILY9pdlMM3Ppi4vDBkY";  //PLAYER URL
        BundleURL2= "https://docs.google.com/uc?export=download&id=1faKphTAPWBpx3YovaPE9fVvtEdO2psFW";  //CUBE URL
        BundleURL3 = "https://docs.google.com/uc?export=download&id=1xIrOuUIX30HS_Dwq2xrP1nqYSIzOIcrT"; //Town URL
#else
    BundleURL = "https://docs.google.com/uc?export=download&id=1faKphTAPWBpx3YovaPE9fVvtEdO2psFW";
#endif
    }

    IEnumerator SaveAssetBundleOnDisk(string URL,string AssetName)
    {
        
        // 에셋 번들을 받아오고자하는 서버의 주소

        // 지금은 주소와 에셋 번들 이름을 함께 묶어 두었지만

        // 주소 + 에셋 번들 이름 형태를 띄는 것이 좋다.
        string uri = URL;

        // 웹 서버에 요청을 생성한다.
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.Send();

        // 에셋 번들을 저장할 경로

        assetBundleDirectory = Application.persistentDataPath + "/AssetBundles";


        // 에셋 번들을 저장할 경로의 폴더가 존재하지 않는다면 생성시킨다.
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        // 파일 입출력을 통해 받아온 에셋을 저장하는 과정
        FileStream fs = new FileStream(assetBundleDirectory + "/" + AssetName+".unity3d", System.IO.FileMode.Create);
        fs.Write(request.downloadHandler.data, 0, (int)request.downloadedBytes);
        fs.Close();

        Debug.Log(AssetName + " 번들 다운로드 완료");

    }



    IEnumerator LoadAssetBundle_Android(string URL)
    {
        string uri = URL;
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

    IEnumerator TestLoadAssetBundle_Android(string URL)
    {
        string uri = URL;
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri, 0);
        yield return request.Send();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        AssetBundleRequest bundleRequest = bundle.LoadAssetAsync("Riko",
            typeof(GameObject));
        yield return request;
        GameObject obj = Instantiate(bundleRequest.asset) as GameObject;
        obj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    IEnumerator LoadBundleScene(string URL,string AssetName)
    {

        string uri = URL;
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(uri, 0);
        yield return request.Send();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        AssetBundleRequest bundleRequest = bundle.LoadAssetAsync(AssetName,
            typeof(GameObject));
        yield return request;
        GameObject obj = Instantiate(bundleRequest.asset) as GameObject;
        obj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

    }

   


    public void LoginBtn()
    {

        Debug.Log("개발용 로그인");
        AccountInfo.Login("12341234", "123123");
    
    }


    public void LoadDungeon()
    {
        LoadingScene.LoadScene(LoadType.WoodDungeon, 1);
    }

    public void LoadScene()
    {
        LoadingScene.LoadScene(LoadType.VillageCheckDownLoad, 0);

    }
    public void MoveNextScene(string sceneName)
    {
        SceneManager.LoadScene("Test_AssetBundleLoad");
    }
    public void LoadAssets()
    {
#if UNITY_ANDROID
        StartCoroutine(TestLoadAssetBundle_Android(BundleURL));
        StartCoroutine(LoadAssetBundle_Android(BundleURL2));
      //  StartCoroutine(TestLoadAssetBundle_Android(BundleURL3));
        StartCoroutine(SaveAssetBundleOnDisk(BundleURL,"Riko"));
        StartCoroutine(SaveAssetBundleOnDisk(BundleURL2,"Cube"));
        DebugLog.SaveLog(this, "PlayerID:" + 123123);
#else
        StartCoroutine(LoadAssetBundle());
        DebugLog.SaveLog(this, "PlayerID:" + 123123);
#endif
    }

}
