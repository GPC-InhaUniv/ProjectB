using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// http://wergia.tistory.com/36?category=737654
/// </summary>

public enum AreaType
{
    Null,
    Town,
    WoodDungeon,
    IronDungeon,
    BrickDungeon,
    SheepDungeon,
}

public class Test_AssetBundleManager : Singleton<Test_AssetBundleManager>
{

    protected Test_AssetBundleManager() { }

    public string AssetName;

    //public string path;

    public AssetBundle PlayerBundle;
    public AssetBundle PublicAssetBundle;
    public AssetBundle Area;
    public AreaType currentArea;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(LoadedAssetBundles());
    }

    public void LoadArea(AreaType areaType)
    {
        if (currentArea == areaType)
            return;
        string bundleName;
        switch (areaType)
        {
            case AreaType.Town:
                bundleName = "townbundle";
                break;
            case AreaType.WoodDungeon:
                bundleName = "wooddungeonbundle";
                break;
            case AreaType.IronDungeon:
                bundleName = "irondungeonbundle";
                break;
            case AreaType.BrickDungeon:
                bundleName = "brickdungeonbundle";
                break;
            case AreaType.SheepDungeon:
                bundleName = "sheepdungeonbundle";
                break;
            default:
                bundleName = null;
                break;
        }
        currentArea = areaType;
        StartCoroutine(LoadAssetBundle(bundleName));
    }

    string SetPath(string assetName)
    {
        return Application.persistentDataPath + "/AssetBundles/" + assetName;// + ".unity3d";
    }
    
    IEnumerator LoadedAssetBundles()
    {
        PlayerBundle = AssetBundle.LoadFromFile(SetPath("plyaerbundle"));
        PublicAssetBundle = AssetBundle.LoadFromFile(SetPath("publicbundles"));
        if (PlayerBundle == null ||PublicAssetBundle == null)
        {
            Debug.Log("Fail");
            yield break;
        }
        else
            Debug.Log("Successe");
    }

    IEnumerator LoadAssetBundle(string areaType)
    {
        
        if (areaType == null)
            yield break;
        if (Area != null)
        {
            Area.Unload(true);
            Area = null;
        }

        Area = AssetBundle.LoadFromFile(SetPath(areaType));
        if (Area == null)
        {
            Debug.Log("Fail");
            yield break;
        }
        else
            Debug.Log("Successe");
    }

    public GameObject LoadObject()
    {
        GameObject gameObject;
        gameObject = Instantiate(Area.LoadAsset(AssetName) as GameObject);
        return gameObject;
    }

    public void LoadScene()
    {
        string[] scene = Area.GetAllScenePaths();
        string loadScenePath = null;

        foreach(string sname in scene)
        {
            if(sname.Contains(AssetName))
            {
                loadScenePath = sname;
            }
        }

        if (loadScenePath == null)
            return;

        SceneManager.LoadScene(loadScenePath);
    }

}
