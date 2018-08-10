using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectB.GameManager
{
    public enum LoadType
    {
        Village,
        BrickDungeon,
        WoodDungeon,
        SheepDungeon,
        IronDungeon,
        VillageCheckDownLoad
    }

    enum SceneName
    {
        Test_Login,
        Test_Loading,
        Test_Empty,
    }
    public class LoadingSceneManager : MonoBehaviour
    {


        public static string NextScene;
        string assetBundleDirectory;
        string currentAssetName = "";
        string BundleURL;
        string brickDungeonBundle;
        string BundleURL3;
        string townBundle;

        int totalBundleCount = 4;
        static int userBundleCount = 0;
        static LoadType currentType;
        static int currentDungeonIndex = 0;

        bool IsDownLoadDone = false;
        bool IsAssetLoadDone = false;
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
        void Awake()
        {
        
            if (currentType.Equals(LoadType.VillageCheckDownLoad) && userBundleCount < totalBundleCount - 1)
            {
                IsDownLoadDone = false;
                Debug.Log("다운로드 필요");
                currentAssetName = "게임 준비중...";
                BundleURL = "https://docs.google.com/uc?export=download&id=10KRqu8GtuwEi-ILY9pdlMM3Ppi4vDBkY";  //PLAYER URL
                brickDungeonBundle = "https://docs.google.com/uc?export=download&id=1zTL1Am6x_hg1VqJXxt1OF-fE-hBin6af";  //BrickDungeon URL
                townBundle = "https://docs.google.com/uc?export=download&id=1CceJkvGreptcoZsbS2YMUOpXmCZkjmG7"; //Town URL

                StartCoroutine(SaveAssetBundleOnDisk(BundleURL, "Riko"));
                StartCoroutine(SaveAssetBundleOnDisk(brickDungeonBundle, "brickdungeonbundle"));
                StartCoroutine(SaveAssetBundleOnDisk(townBundle, "Town"));

            }
            else
            {
                Debug.Log("다운로드 불필요");
                totalBundleCount = 1;
                IsDownLoadDone = true;

            }

            StartCoroutine(CheckDownLoadDone());

            StartCoroutine(LoadScene());

        }

        IEnumerator CheckDownLoadDone()
        {
            while (!IsAssetLoadDone)
            {
                if (IsDownLoadDone)
                {
                    switch (currentType)
                    {
                        case LoadType.Village:
                            break;
                        case LoadType.BrickDungeon:
                            currentAssetName = "흙 던전 로드중..";
                            GameObject tempObject = Test_PoolManager.Instance.GetArea();
                            Destroy(tempObject);
                          Test_AssetBundleManager.Instance.LoadArea(AreaType.BrickDungeon);
            
                            Test_PoolManager.Instance.SetArea(Test_AssetBundleManager.Instance.LoadObject(BundleType.Area, "BrickDungeon1"));
                        //    Test_AssetBundleManager.Instance.LoadObject(BundleType.Area);
                            break;
                        case LoadType.WoodDungeon:
                            currentAssetName = "나무 던전 로드중..";
                            
                            break;
                        case LoadType.SheepDungeon:
                            break;
                        case LoadType.IronDungeon:
                            break;
                        case LoadType.VillageCheckDownLoad:
                            currentAssetName = "마을 로드중...";
                            Test_AssetBundleManager.Instance.LoadArea(AreaType.Town);
                           
                           Test_PoolManager.Instance.SetPlayer(Test_AssetBundleManager.Instance.LoadObject(BundleType.Player,"Riko"));
    
                            Test_PoolManager.Instance.SetArea(Test_AssetBundleManager.Instance.LoadObject(BundleType.Area,"Village"));
                            break;
                    }

                    IsAssetLoadDone = true;
                }

                else
                {
                    yield return new WaitForSeconds(0.3f);
                }
            }
            yield return null;
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
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f / totalBundleCount, timer);
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
                        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, asyncOperation.progress / totalBundleCount, timer);

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
                GameControllManager.Instance.SetObjectPosition();
                asyncOperation.allowSceneActivation = true;
            }

        }


        public static void LoadScene(LoadType mapType, int index)
        {
            switch (mapType)
            {
                case LoadType.Village:
                    currentType = LoadType.Village;
                    NextScene = SceneName.Test_Empty.ToString();
                    break;
                case LoadType.BrickDungeon:
                    currentType = LoadType.BrickDungeon;
                    NextScene = SceneName.Test_Empty.ToString();
                    break;
                case LoadType.WoodDungeon:
                    currentType = LoadType.WoodDungeon;
                    NextScene = SceneName.Test_Empty.ToString();
                    break;
                case LoadType.SheepDungeon:
                    currentType = LoadType.SheepDungeon;
                    break;
                case LoadType.IronDungeon:
                    currentType = LoadType.IronDungeon;
                    break;
                case LoadType.VillageCheckDownLoad:
                    currentType = LoadType.VillageCheckDownLoad;
                    userBundleCount = CheckDownLoadFile();
                    NextScene = SceneName.Test_Empty.ToString();
                    break;
                default:
                    break;
            }

            currentDungeonIndex = index;
            SceneManager.LoadScene(SceneName.Test_Loading.ToString());


        }




        static int CheckDownLoadFile()
        {
            try
            {
                // Only get files that begin with the letter "c."
                string[] dirs = Directory.GetFiles(Application.persistentDataPath + "/AssetBundles", "*_unity3D");
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
            FileStream fs = new FileStream(assetBundleDirectory + "/" + AssetName + "_unity3D", System.IO.FileMode.Create, FileAccess.Write);

            fs.Write(request.downloadHandler.data, 0, (int)request.downloadedBytes);

            fs.Close();

            totalBundleCount--;

            if (totalBundleCount <= 0)
            {
                totalBundleCount = 1;

            }

            if (totalBundleCount == 1)
            {
                IsDownLoadDone = true;
                Debug.Log("모든 파일 다운로드 완료");
            }
            currentAssetName = AssetName + " 다운로드 중...";

            Debug.Log(AssetName + " 번들 다운로드 완료 " + totalBundleCount);
        }

    }
}