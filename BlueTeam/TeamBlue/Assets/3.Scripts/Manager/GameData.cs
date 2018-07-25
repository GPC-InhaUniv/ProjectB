
using System;
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

    public TownInformation(int relations,int lastclearQuest)
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
    public DevelopmentCard DevelopmentCards;

    public PlayerInformation(int playerlevel,int playerexp,int portioncount,int one,int two,int three,int four)
    {
        PlayerLevel = playerlevel;
        PlayerExp = playerexp;
        PortionCount = portioncount;
        DevelopmentCards = new DevelopmentCard(one,two,three,four);
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

    public DevelopmentCard(int one,int two,int three,int four)
    {
        One = one;
        Two = two;
        Three = three;
        Four=four;
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

    public Item(int brick,int wood,int iron,int sheep,int specialItem)
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
    public Item inventoryItems;
    public  Item wareHouseItems;

    /*NOTICE*/
    /* 0= Brick, 1=Wood, 2=Iron 3=Sheep*/
    public int[] lastCleardDungeonNum;




    private void Start()
    { 
        inventoryItems = new Item(0,0,0,0,0);
        wareHouseItems = new Item(0, 0, 0, 0, 0);
        AtownInformation = new TownInformation(0,0);
        BtownInformation = new TownInformation(0, 0);
        playerInfomation = new PlayerInformation(0,0,0,0,0,0,0);
        equipment = new Equipment(MAXITEMCOUNT);

        lastCleardDungeonNum = new int[MAXDUNGEONCOUNT];
   
        for (int i=0;i<MAXDUNGEONCOUNT;i++)
        {
            lastCleardDungeonNum[i] = 0;
        }

    }

}
