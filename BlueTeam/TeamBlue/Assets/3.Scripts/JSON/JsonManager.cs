
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    string assetBundleDirectory;
    string playerInformationLv;
    string playerInformationExp;
    string playerInformationPortionCount;

    private void Start()
    {
#if UNITY_ANDROID
        assetBundleDirectory = Application.persistentDataPath + "/10.JsonFolder";
#else
        assetBundleDirectory="Assets/10.JsonFolder";
#endif
    }

    public void PlusPortion()
    {
        GameData.Instance.playerInfomation.PortionCount++;
    }

    //제이슨 파일 저장
    public void OnClickSaveJSONBtn()
    {
        Data mydata = new Data();
        string save = JsonUtility.ToJson(mydata, prettyPrint: true);
        Debug.Log(save);
        WriteStringToFile(save, "save.json");

        ClientSetUserPublisherData();

    }

    public void ClientSetUserPublisherData()
    {
        string load = ReadStringFromFile("save.json");
        var loadData = JsonUtility.FromJson<Data>(load);
        playerInformationLv = loadData.playerInfomation.PlayerLevel.ToString();
        playerInformationExp = loadData.playerInfomation.PlayerExp.ToString();
        playerInformationPortionCount = loadData.playerInfomation.PortionCount.ToString();

        PlayFabClientAPI.UpdateUserPublisherData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
                {"playerInformationLv", playerInformationLv},
                {"playerInformationExp",playerInformationExp },
                {"playerInformationPortionCount",playerInformationPortionCount },

         }
        },
        result => Debug.Log("Complete setting Regular User Publisher Data"),
        error =>
        {
            Debug.Log("Error setting Regular User Publisher Data");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    
    //제이슨 파일 로드
    public void OnClickLoadJSONBtn()
    {
        string load = ReadStringFromFile("save.json");
        var loadData = JsonUtility.FromJson<Data>(load);
        Debug.Log(load);

        ClientGetUserPublisherData();
    }

    // Use client API to get Regular User Publisher Data for selected user 
    public void ClientGetUserPublisherData()
    {
        PlayFabClientAPI.GetUserPublisherData(new GetUserDataRequest()
        {

        }, result =>
        {
            if (result.Data == null || !result.Data.ContainsKey("playerInformationPortionCount")) Debug.Log("No playerInformationPortionCount");
            else Debug.Log("playerInformationPortionCount: " + result.Data["playerInformationPortionCount"].Value);


        },
        error =>
        {
            Debug.Log("Got error getting Regular Publisher Data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    private string ReadStringFromFile(string path)
    {
        string text = System.IO.File.ReadAllText(assetBundleDirectory + "/" + path);

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



