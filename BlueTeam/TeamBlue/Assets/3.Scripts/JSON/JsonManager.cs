
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    string assetBundleDirectory;
    string playerInformation;
    private void Start()
    {
#if UNITY_ANDROID
        assetBundleDirectory = Application.persistentDataPath+ "/10.JsonFolder";
#else
        assetBundleDirectory="Assets/10.JsonFolder";
#endif
    }


    //제이슨 파일 저장
    public void OnClickSaveJSONBtn()
    {
        Data mydata = new Data();
        string save = JsonUtility.ToJson(mydata, prettyPrint: true);
        Debug.Log(save);


        WriteStringToFile(save, "save.json");


    }

    //제이슨 파일 로드
    public void OnClickLoadJSONBtn()
    {
        string load = ReadStringFromFile("save.json");
        var loadData = JsonUtility.FromJson<Data>(load);
        playerInformation = loadData.playerInfomation.PlayerLevel.ToString();
        Debug.Log(load);
        Debug.Log(playerInformation);
       
    }

    private string ReadStringFromFile(string path)
    {
        string text = System.IO.File.ReadAllText(assetBundleDirectory+ "/" + path);

        return text;
    }


    private void WriteStringToFile(string text, string path)
    {

        // 에셋 번들을 저장할 경로의 폴더가 존재하지 않는다면 생성시킨다.
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }


        using (System.IO.StreamWriter file =
             new System.IO.StreamWriter(assetBundleDirectory + "/" + path, false))
        {
            file.WriteLine(text);
        }

    }
}


[SerializeField]
public class Data
{
    public PlayerInformation playerInfomation;
    public TownInformation AtownInformation;
    public TownInformation BtownInformation;
    public Equipment equipment;
    public Item inventoryItems;
    public Item wareHouseItems;

    public Data()
    {
        playerInfomation = GameData.Instance.playerInfomation;
        AtownInformation = GameData.Instance.AtownInformation;
        BtownInformation = GameData.Instance.BtownInformation;
        equipment = GameData.Instance.equipment;
        inventoryItems = GameData.Instance.inventoryItems;
        wareHouseItems = GameData.Instance.wareHouseItems;
    }
}



