
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    string assetBundleDirectory;
    public AccountInfo Info;
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
        GameData.Instance.playerInfomation.PlayerLevel+=2;
        GameData.Instance.playerInfomation.PortionCount+=5;
        GameData.Instance.inventoryItems.Brick += 10;
    }

    //제이슨 파일 저장
    public void OnClickSaveJSONBtn()
    {
        Data mydata = new Data();
        string save = JsonUtility.ToJson(mydata, prettyPrint: true);
        Debug.Log(save);
        WriteStringToFile(save, "save.json");

        SetGameData(mydata);
     //   ClientSetUserPublisherData();

    }

    void SetGameData(Data mydata)
    {
        string playerLv="";
        string playerExp = "";
        string playerPortionCount = "";

        string AtownRelationship = "";
        string AtownQuest = "";
        string BtownRelationship = "";
        string BtownQuest = "";

        string EquipmentWeapon = "";
        string EquipmentArmor = "";
        string EquipmentHat = "";

        string InventoryItems = "";
        string WareHouseItems = "";

        playerLv = mydata.playerInfomation.PlayerLevel.ToString();
        playerExp = mydata.playerInfomation.PlayerExp.ToString();
        playerPortionCount = mydata.playerInfomation.PortionCount.ToString();
        AtownRelationship = mydata.AtownInformation.RelationsShip.ToString();
        AtownQuest = mydata.AtownInformation.LastCleardQuest.ToString();
        BtownRelationship = mydata.BtownInformation.RelationsShip.ToString();
        BtownQuest = mydata.BtownInformation.LastCleardQuest.ToString();

        for(int i=0;i<GameData.MAXITEMCOUNT;i++)
        {
            if (mydata.equipment.Weapon[i])
                EquipmentWeapon += 1;
            else EquipmentWeapon += 0;

            if (mydata.equipment.Armor[i])
                EquipmentArmor += 1;
            else EquipmentArmor += 0;
            if (mydata.equipment.Hat[i])
                EquipmentHat += 1;
            else EquipmentHat += 0;
        }

        InventoryItems = mydata.inventoryItems.Brick.ToString()+"/"+ mydata.inventoryItems.Wood.ToString() + "/"
            + mydata.inventoryItems.Iron.ToString() + "/"+mydata.inventoryItems.Sheep.ToString() + "/"+mydata.inventoryItems.SpecialItem;

        WareHouseItems = mydata.wareHouseItems.Brick.ToString() + "/" + mydata.wareHouseItems.Wood.ToString() + "/"
            + mydata.wareHouseItems.Iron.ToString() + "/" + mydata.wareHouseItems.Sheep.ToString() + "/" + mydata.wareHouseItems.SpecialItem;



        Dictionary<string, string> data = new Dictionary<string, string>();

        data.Add("PlayerInformation", playerLv + "/" + playerExp + "/" + playerPortionCount);
        data.Add("ATownInformation", AtownRelationship + "/" + AtownQuest);
        data.Add("BTownInformation", BtownRelationship + "/" + BtownQuest );
        data.Add("Equipment",EquipmentWeapon+"/"+ EquipmentArmor+ "/"+ EquipmentHat);
        data.Add("InventoryItems", InventoryItems);
        data.Add("WareHouseItems", WareHouseItems);

        UpdateUserDataRequest request = new UpdateUserDataRequest()
        {
            Data = data,

        };
        PlayFabClientAPI.UpdateUserData(request, UpdateDataInfo, GameErrorManager.OnAPIError);

    }

    void UpdateDataInfo(UpdateUserDataResult result)
    {
        Debug.Log("UpdateDataInfo");      
    }

     
    //제이슨 파일 로드
    public void OnClickLoadJSONBtn()
    {

        string load = ReadStringFromFile("save.json");
        var loadData = JsonUtility.FromJson<Data>(load);
        Debug.Log(load);
        GetGameData();
    }

    public void GetGameData()
    {
        string playerInformation="";
        string equipment = "";
        string inventoryitem = "";
        string warehouseitem = "";
        UserDataRecord userData = new UserDataRecord();
        AccountInfo.Instance.Info.UserData.TryGetValue("PlayerInformation", out userData);
        playerInformation = userData.Value;

        AccountInfo.Instance.Info.UserData.TryGetValue("Equipment", out userData);
        equipment = userData.Value;

        AccountInfo.Instance.Info.UserData.TryGetValue("InventoryItems", out userData);
        inventoryitem = userData.Value;

        AccountInfo.Instance.Info.UserData.TryGetValue("WareHouseItems", out userData);
        warehouseitem = userData.Value;

        string[] playerInformationArray = playerInformation.Split('/');
        string[] equipmentArray = equipment.Split('/');
        string[] inventoryItemArray = inventoryitem.Split('/');
        string[] warehouseItemArray = warehouseitem.Split('/');

        //playerinfo load
        GameData.Instance.playerInfomation.PlayerLevel =Convert.ToInt32(playerInformationArray[0]);
        GameData.Instance.playerInfomation.PlayerExp = Convert.ToInt32(playerInformationArray[1]);
        GameData.Instance.playerInfomation.PortionCount = Convert.ToInt32(playerInformationArray[2]);

        //equipment load
        for (int i = 0; i < GameData.MAXITEMCOUNT; i++)
        {
            if (equipmentArray[0][i].Equals('0'))
                GameData.Instance.equipment.Weapon[i] = false;
            else GameData.Instance.equipment.Weapon[i] = true;

            if (equipmentArray[1][i].Equals('0'))
                GameData.Instance.equipment.Armor[i] = false;
            else GameData.Instance.equipment.Armor[i] = true;

            if (equipmentArray[2][i].Equals('0'))
                GameData.Instance.equipment.Hat[i] = false;
            else GameData.Instance.equipment.Hat[i] = true;

        }

        //itemload
        GameData.Instance.inventoryItems.Brick = Convert.ToInt32(inventoryItemArray[0]);
        GameData.Instance.inventoryItems.Wood = Convert.ToInt32(inventoryItemArray[1]);
        GameData.Instance.inventoryItems.Iron = Convert.ToInt32(inventoryItemArray[2]);
        GameData.Instance.inventoryItems.Sheep = Convert.ToInt32(inventoryItemArray[3]);
        GameData.Instance.inventoryItems.SpecialItem = Convert.ToInt32(inventoryItemArray[4]);

        GameData.Instance.wareHouseItems.Brick = Convert.ToInt32(warehouseItemArray[0]);
        GameData.Instance.wareHouseItems.Wood = Convert.ToInt32(warehouseItemArray[1]);
        GameData.Instance.wareHouseItems.Iron = Convert.ToInt32(warehouseItemArray[2]);
        GameData.Instance.wareHouseItems.Sheep = Convert.ToInt32(warehouseItemArray[3]);
        GameData.Instance.wareHouseItems.SpecialItem = Convert.ToInt32(warehouseItemArray[4]);


        //Town Relationship and quest load


        Debug.Log(GameData.Instance.playerInfomation);

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



