using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MapType
{
    Village,
    BrickDungeon,
    WoodDungeon,
    SheepDungeon,
    IronDungeon,
}
public class LoadingScene : MonoBehaviour
{


    public static string NextScene;
    string nextSceneName;
    string assetBundleDirectory;
    string currentAssetName = "";
    string BundleURL;
    string BundleURL2;
    string BundleURL3;

    static int totalBundleCount = 4;
    static int userBundleCount = 0;

    int percentage;

    [SerializeField]
    Image progressBar;
    [SerializeField]
    Text progressText;
    [SerializeField]
    Text currentBundleText;
    
    public delegate void LoadInGameScene();

    public LoadInGameScene LoadInGameSceneDelegater;

    // Use this for initialization
    private void Awake()
    {
        if (userBundleCount != totalBundleCount - 1)
        {
            Debug.Log("다운로드 필요");
            currentAssetName = "게임 준비중...";
            BundleURL = "https://docs.google.com/uc?export=download&id=10KRqu8GtuwEi-ILY9pdlMM3Ppi4vDBkY";  //PLAYER URL
            BundleURL2 = "https://docs.google.com/uc?export=download&id=1faKphTAPWBpx3YovaPE9fVvtEdO2psFW";  //CUBE URL
            BundleURL3 = "https://docs.google.com/uc?export=download&id=1xIrOuUIX30HS_Dwq2xrP1nqYSIzOIcrT"; //Town URL

            StartCoroutine(SaveAssetBundleOnDisk(BundleURL, "Riko"));
            StartCoroutine(SaveAssetBundleOnDisk(BundleURL2, "Cube"));
            StartCoroutine(SaveAssetBundleOnDisk(BundleURL3, "Villige_Sence"));
        }
        else
        {
            Debug.Log("다운로드 불필요");
            totalBundleCount = 1;
        }

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(NextScene);
        asyncOperation.allowSceneActivation = false;

        float timer = 0.0f;
        while (!asyncOperation.isDone)  //종료되기 전까지 while문 실행
        {       
            yield return null;

            timer += Time.deltaTime;

            if (asyncOperation.progress >= 0.9f)
            {
                currentBundleText.text = currentAssetName;
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f/totalBundleCount, timer);
                percentage = Convert.ToInt32(progressBar.fillAmount * 100);
                progressText.text = percentage.ToString() + "%";

                if (progressBar.fillAmount >= 0.94f) //가득 찼다면
                {
                    currentAssetName = "완료!";
                    progressText.text = "100%";
                    LoadMainScene(asyncOperation);

                }

                else
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, asyncOperation.progress/ totalBundleCount, timer);

                    if (progressBar.fillAmount > asyncOperation.progress)
                    {
                       timer = 0.0f;
                    }
                }
            }
        }
    }

    void LoadMainScene(AsyncOperation asyncOperation)
    {

        //pressAnyKeyText.text = "Press Any Key To Start Game!";

        //pressAnyKeyText.gameObject.SetActive(true);
        //progressBar.gameObject.SetActive(false);
      
            if (Input.anyKeyDown)
            {
               
                asyncOperation.allowSceneActivation = true;
               
            }
        

    }


    public static void LoadScene(MapType mapType,int index)
    {
        //if (sceneName == "Main")
        //    IsMainSceneLoading = true;
        //else
        //    IsMainSceneLoading = false;

        switch (mapType)
        {
            case MapType.Village:
                userBundleCount = CheckDownLoadFile();
                NextScene = "Test_AssetBundleLoad";
                SceneManager.LoadScene("Test_LoadingScene");
                break;
            case MapType.BrickDungeon:
                break;
            case MapType.WoodDungeon:
                break;
            case MapType.SheepDungeon:
                break;
            case MapType.IronDungeon:
                break;
            default:
                break;
        }
      
      
       
    }


    private static int CheckDownLoadFile()
    {
        try
        {
            // Only get files that begin with the letter "c."
            string[] dirs = Directory.GetFiles(Application.persistentDataPath + "/AssetBundles", "*.unity3D");
            Debug.Log("현재 파일 개수:" + dirs.Length);
            return dirs.Length;
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
           
        }

        return -1;

    }




    IEnumerator SaveAssetBundleOnDisk(string URL, string AssetName)
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
        FileStream fs = new FileStream(assetBundleDirectory + "/" + AssetName + ".unity3d", System.IO.FileMode.Create);
        
        fs.Write(request.downloadHandler.data, 0, (int)request.downloadedBytes);
       
        fs.Close();

        totalBundleCount--;
        if (totalBundleCount <= 0)
            totalBundleCount = 1;

        currentAssetName = AssetName + " 찾아오는중...";

        Debug.Log(AssetName + " 번들 다운로드 완료 " + totalBundleCount);
    }

}
