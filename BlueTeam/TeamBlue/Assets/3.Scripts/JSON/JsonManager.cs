
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    string assetBundleDirectory;

    void Start()
    {

#if UNITY_ANDROID
        assetBundleDirectory = Application.persistentDataPath + "/10.JsonFolder";
#else
        assetBundleDirectory="Assets/10.JsonFolder";
#endif
    }

   

    //제이슨 파일 저장
    public void OnClickSaveJSONBtn()
    {
        GameData.Instance.SetGameDataToServer();
        Data mydata = new Data();
        string save = JsonUtility.ToJson(mydata, prettyPrint: true);
        Debug.Log(save);
        WriteStringToFile(save, "save.json");

    }


    //제이슨 파일 로드
    public void OnClickLoadJSONBtn()
    {
        GameData.Instance.GetGameDataFromServer();
        string load = ReadStringFromFile("save.json");
        var loadData = JsonUtility.FromJson<Data>(load);
        Debug.Log(load);
        
    }

   

    string ReadStringFromFile(string path)
    {
        string text = System.IO.File.ReadAllText(assetBundleDirectory + "/" + path);

        return text;
    }


    void WriteStringToFile(string text, string path)
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
    public PlayerInformation PlayerInfomation;
    public TownInformation ATownInformation;
    public TownInformation BTownInformation;
    public Equipment Equipment;
    public EquipmentCombination EquipmentCombination;
    public Item InventoryItems;
    public Item WareHouseItems;
    public DevelopmentCard Card;
    

    public Data()
    {
        PlayerInfomation = GameData.Instance.playerInfomation;
        ATownInformation = GameData.Instance.AtownInformation;
        BTownInformation = GameData.Instance.BtownInformation;
        Equipment = GameData.Instance.equipment;
        InventoryItems = GameData.Instance.inventoryItem;
        WareHouseItems = GameData.Instance.wareHouseItem;
        Card = GameData.Instance.card;
        EquipmentCombination = GameData.Instance.equipmentCombination;
    }
}



