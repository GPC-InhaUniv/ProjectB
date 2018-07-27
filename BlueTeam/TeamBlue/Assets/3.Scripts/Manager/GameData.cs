
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameResources
{
    Brick,
    Wood,
    Iron,
    Sheep,
    SpecialItem,
}

[Serializable]
public struct TownInformation
{
    public int RelationsShip;
    public int LastCleardQuest;

    public TownInformation(int relations, int lastclearQuest)
    {
        RelationsShip = relations;
        LastCleardQuest = lastclearQuest;
    }
}

[Serializable]
public struct PlayerInformation
{
    public int PlayerLevel;
    public float PlayerExp;
    public int PortionCount;

    public PlayerInformation(int playerlevel, int playerexp, int portioncount)
    {
        PlayerLevel = playerlevel;
        PlayerExp = playerexp;
        PortionCount = portioncount;
      
    }
}

[Serializable]
public struct Equipment
{
    public bool[] Weapon;
    public bool[] Armor;
    public bool[] Hat;

    public Equipment(int maxItemCount)
    {
        Weapon = new bool[maxItemCount];
        Armor = new bool[maxItemCount];
        Hat = new bool[maxItemCount];
    }
}

[Serializable]
public struct DevelopmentCard
{
    public int One;
    public int Two;
    public int Three;
    public int Four;

    public DevelopmentCard(int one, int two, int three, int four)
    {
        One = one;
        Two = two;
        Three = three;
        Four = four;
    }
}

[Serializable]
public struct Item
{
    public int Brick;
    public int Wood;
    public int Iron;
    public int Sheep;
    public int SpecialItem;

    public Item(int brick, int wood, int iron, int sheep, int specialItem)
    {
        Brick = brick;
        Wood = wood;
        Iron = iron;
        Sheep = sheep;
        SpecialItem = specialItem;
    }
}


public class GameData : Singleton<GameData>
{
    public const int MAXDUNGEONCOUNT = 4;
    public const int MAXITEMCOUNT = 9;
    public PlayerInformation playerInfomation;
    public TownInformation AtownInformation;
    public TownInformation BtownInformation;
    public Equipment equipment;
    public Item inventoryItem;
    public Item wareHouseItem;
    public DevelopmentCard card;

    /*NOTICE*/
    /* 0= Brick, 1=Wood, 2=Iron 3=Sheep*/
    public int[] lastCleardDungeonNum;

    AccountInfo Info;
    string[] playerInformationArray;
    string[] equipmentArray;
    string[] inventoryItemArray;
    string[] warehouseItemArray;
    string[] townInformationArray;
    string[] cardInformationArray;

    void Start()
    {
        inventoryItem = new Item(0, 0, 0, 0, 0);
        wareHouseItem = new Item(0, 0, 0, 0, 0);
        AtownInformation = new TownInformation(0, 0);
        BtownInformation = new TownInformation(0, 0);
        playerInfomation = new PlayerInformation(0, 0, 0);
        equipment = new Equipment(MAXITEMCOUNT);
        card = new DevelopmentCard(0, 0, 0, 0);

        lastCleardDungeonNum = new int[MAXDUNGEONCOUNT];

        for (int i = 0; i < MAXDUNGEONCOUNT; i++)
        {
            lastCleardDungeonNum[i] = 0;
        }

    }

    public void PlusPortion()
    {
        playerInfomation.PortionCount++;
        playerInfomation.PlayerLevel += 2;
        playerInfomation.PortionCount += 5;
        inventoryItem.Brick += 10;
        AtownInformation.RelationsShip++;
        BtownInformation.LastCleardQuest = 4;
        card.One++;
        card.Four++;

    }
    public void SetGameDataToServer()
    {
        string playerLv = "";
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

        string Cards = "";

        playerLv = playerInfomation.PlayerLevel.ToString();
        playerExp = playerInfomation.PlayerExp.ToString();
        playerPortionCount = playerInfomation.PortionCount.ToString();
        AtownRelationship = AtownInformation.RelationsShip.ToString();
        AtownQuest = AtownInformation.LastCleardQuest.ToString();
        BtownRelationship = BtownInformation.RelationsShip.ToString();
        BtownQuest = BtownInformation.LastCleardQuest.ToString();
       
        for (int i = 0; i < MAXITEMCOUNT; i++)
        {
            if (equipment.Weapon[i])
                EquipmentWeapon += 1;
            else EquipmentWeapon += 0;

            if (equipment.Armor[i])
                EquipmentArmor += 1;
            else EquipmentArmor += 0;
            if (equipment.Hat[i])
                EquipmentHat += 1;
            else EquipmentHat += 0;
        }

        InventoryItems = inventoryItem.Brick.ToString() + "/" + inventoryItem.Wood.ToString() + "/"
            + inventoryItem.Iron.ToString() + "/" + inventoryItem.Sheep.ToString() + "/" + inventoryItem.SpecialItem;

        WareHouseItems = wareHouseItem.Brick.ToString() + "/" + wareHouseItem.Wood.ToString() + "/"
            + wareHouseItem.Iron.ToString() + "/" + wareHouseItem.Sheep.ToString() + "/" + wareHouseItem.SpecialItem;

        Cards = card.One.ToString() + "/" + card.Two.ToString() + "/" + card.Three.ToString() + "/" + card.Four.ToString();


        Dictionary<string, string> data = new Dictionary<string, string>();

        data.Add("PlayerInformation", playerLv + "/" + playerExp + "/" + playerPortionCount);
        data.Add("TownInformation", AtownRelationship + "/" + AtownQuest+ "/"+BtownRelationship +"/"+BtownQuest);
        data.Add("Equipment", EquipmentWeapon + "/" + EquipmentArmor + "/" + EquipmentHat);
        data.Add("InventoryItems", InventoryItems);
        data.Add("WareHouseItems", WareHouseItems);
        data.Add("Card", Cards);

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


    public void GetGameDataFromServer()
    {
        string tempPlayerInformation = "";
        string tempEquipments = "";
        string tempInventoryitems = "";
        string tempWarehouseitems = "";
        string tempTownInformations = "";
        string tempCards = "";

        UserDataRecord userData = new UserDataRecord();
        AccountInfo.Instance.Info.UserData.TryGetValue("PlayerInformation", out userData);
        tempPlayerInformation = userData.Value;

        AccountInfo.Instance.Info.UserData.TryGetValue("Equipment", out userData);
        tempEquipments = userData.Value;

        AccountInfo.Instance.Info.UserData.TryGetValue("InventoryItems", out userData);
        tempInventoryitems = userData.Value;

        AccountInfo.Instance.Info.UserData.TryGetValue("WareHouseItems", out userData);
        tempWarehouseitems = userData.Value;

        AccountInfo.Instance.Info.UserData.TryGetValue("TownInformation", out userData);
        tempTownInformations = userData.Value;

        AccountInfo.Instance.Info.UserData.TryGetValue("Card", out userData);
        tempCards = userData.Value;


        playerInformationArray = tempPlayerInformation.Split('/');
        equipmentArray = tempEquipments.Split('/');
        inventoryItemArray = tempInventoryitems.Split('/');
        warehouseItemArray = tempWarehouseitems.Split('/');
        townInformationArray = tempTownInformations.Split('/');
        cardInformationArray = tempCards.Split('/');

        //playerinfo load
        playerInfomation.PlayerLevel = Convert.ToInt32(playerInformationArray[0]);
        playerInfomation.PlayerExp = Convert.ToInt32(playerInformationArray[1]);
        playerInfomation.PortionCount = Convert.ToInt32(playerInformationArray[2]);

        //equipment load
        for (int i = 0; i < MAXITEMCOUNT; i++)
        {
            if (equipmentArray[0][i].Equals('0'))
                Instance.equipment.Weapon[i] = false;
            else Instance.equipment.Weapon[i] = true;

            if (equipmentArray[1][i].Equals('0'))
                Instance.equipment.Armor[i] = false;
            else Instance.equipment.Armor[i] = true;

            if (equipmentArray[2][i].Equals('0'))
                Instance.equipment.Hat[i] = false;
            else Instance.equipment.Hat[i] = true;

        }

        //itemload
        inventoryItem.Brick = Convert.ToInt32(inventoryItemArray[0]);
        inventoryItem.Wood = Convert.ToInt32(inventoryItemArray[1]);
        inventoryItem.Iron = Convert.ToInt32(inventoryItemArray[2]);
        inventoryItem.Sheep = Convert.ToInt32(inventoryItemArray[3]);
        inventoryItem.SpecialItem = Convert.ToInt32(inventoryItemArray[4]);

        wareHouseItem.Brick = Convert.ToInt32(warehouseItemArray[0]);
        wareHouseItem.Wood = Convert.ToInt32(warehouseItemArray[1]);
        wareHouseItem.Iron = Convert.ToInt32(warehouseItemArray[2]);
        wareHouseItem.Sheep = Convert.ToInt32(warehouseItemArray[3]);
        wareHouseItem.SpecialItem = Convert.ToInt32(warehouseItemArray[4]);


        //Town Relationship and quest load
        AtownInformation.RelationsShip = Convert.ToInt32(townInformationArray[0]);
        AtownInformation.LastCleardQuest = Convert.ToInt32(townInformationArray[1]);
        BtownInformation.RelationsShip = Convert.ToInt32(townInformationArray[2]);
        BtownInformation.LastCleardQuest = Convert.ToInt32(townInformationArray[3]);

        //development card load
        card.One = Convert.ToInt32(cardInformationArray[0]);
        card.Two = Convert.ToInt32(cardInformationArray[1]);
        card.Three = Convert.ToInt32(cardInformationArray[2]);
        card.Four = Convert.ToInt32(cardInformationArray[3]);

        Debug.Log(playerInfomation);

    }



}
