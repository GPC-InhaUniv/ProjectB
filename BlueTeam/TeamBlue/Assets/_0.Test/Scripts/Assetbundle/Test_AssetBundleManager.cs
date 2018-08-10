using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// http://wergia.tistory.com/36?category=737654
/// </summary>
using UnityEngine.UI;
public enum AreaType
{
    Null,
    Town,
    WoodDungeon,
    IronDungeon,
    BrickDungeon,
    SheepDungeon,
}

public enum BundleType
{
    Player,
    Common,
    Area,
}


public class Test_AssetBundleManager : Singleton<Test_AssetBundleManager>
{
    const string PlayerBundleName = "Riko";
    const string CommonBundleName = "townbundle";


    protected Test_AssetBundleManager() { }

    public string AssetName;

    //public string path;
    public Text Log;
    public AssetBundle PlayerBundle;
    public AssetBundle CommonAssetBundle;
    public AssetBundle Area;
    public AreaType currentArea;

    void Start()
    {
      
        DontDestroyOnLoad(gameObject);
     // StartCoroutine(LoadedAssetBundles());
    }

    public void LoadArea(AreaType areaType)
    {
        if (currentArea == areaType)
            return;
        string bundleName;
        switch (areaType)
        {
            case AreaType.Town:
                bundleName = "Town";
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
        return Application.persistentDataPath + "/AssetBundles/" + assetName + "_unity3D";
    }
    
    IEnumerator LoadedAssetBundles()
    {
        PlayerBundle = AssetBundle.LoadFromFile(SetPath(PlayerBundleName));

        CommonAssetBundle = AssetBundle.LoadFromFile(SetPath(CommonBundleName));
        if (PlayerBundle == null ||CommonAssetBundle == null)
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



    public GameObject LoadObject(BundleType bundleType)
    {
        
        GameObject gameObject;
        switch (bundleType)
        {
            case BundleType.Player:
                PlayerBundle = AssetBundle.LoadFromFile(SetPath(PlayerBundleName));
                gameObject = PlayerBundle.LoadAsset(AssetName) as GameObject;
                break;
            case BundleType.Common:
                gameObject = CommonAssetBundle.LoadAsset(AssetName) as GameObject;
                break;
            case BundleType.Area:
                gameObject = Area.LoadAsset(AssetName) as GameObject;
                break;
            default:
                gameObject = null;
                break;
        }
        
        return gameObject;
    }

    

}
