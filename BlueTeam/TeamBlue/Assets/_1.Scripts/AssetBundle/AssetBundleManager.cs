using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AreaType
{
    Null,
    Village,
    WoodDungeon,
    IronDungeon,
    BrickDungeon,
    SheepDungeon,
    BossDungeon,
}

public enum BundleType
{
    Player,
    Monster,
    Common,
    Area,
}

public class AssetBundleManager : Singleton<AssetBundleManager> {
    const string PlayerBundleName = "playerbundle";
    const string CommonBundleName = "commonbundle";
    const string MonsterBundleName = "TestMonster";

    protected AssetBundleManager() { }


    //public string path;
    public Text Log;
    public AssetBundle PlayerBundle;
    public AssetBundle CommonAssetBundle;
    public AssetBundle Area;
    public AssetBundle MonsterBundle;
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
            case AreaType.Village:
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
                bundleName = "brickdungeonbundle";
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
        if (PlayerBundle == null || CommonAssetBundle == null)
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


    public AudioClip LoadSound(BundleType bundleType, string AssetName)
    {
        AudioClip temp;
        if (CommonAssetBundle == null)
            CommonAssetBundle = AssetBundle.LoadFromFile(SetPath(CommonBundleName));
        temp = CommonAssetBundle.LoadAsset<AudioClip>(AssetName);

        return temp;
    }

    public Sprite LoadSprite(BundleType bundleType, string AssetName)
    {
        if (AssetName == null)
            return null;

        Sprite tempSprite;
        if (CommonAssetBundle == null)
            CommonAssetBundle = AssetBundle.LoadFromFile(SetPath(CommonBundleName));
        tempSprite = CommonAssetBundle.LoadAsset<Sprite>(AssetName);

        return tempSprite;
    }

    public GameObject LoadObject(BundleType bundleType, string AssetName)
    {

        GameObject gameObject;
        switch (bundleType)
        {

            case BundleType.Player:
                if (PlayerBundle == null)
                    PlayerBundle = AssetBundle.LoadFromFile(SetPath(PlayerBundleName));
                gameObject = PlayerBundle.LoadAsset<GameObject>(AssetName);
                break;
            case BundleType.Common:
                if (CommonAssetBundle == null)
                    CommonAssetBundle = AssetBundle.LoadFromFile(SetPath(CommonBundleName));
                gameObject = CommonAssetBundle.LoadAsset<GameObject>(AssetName);
                break;
            case BundleType.Area:
                gameObject = Area.LoadAsset<GameObject>(AssetName);
                break;
            default:
                gameObject = null;
                break;
        }

        return gameObject;
    }



}
