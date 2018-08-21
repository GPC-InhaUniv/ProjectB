using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.GameManager
{
    public enum GameResources
    {
        Brick,
        Wood,
        Iron,
        Sheep,
        SpecialItem,
    }

    /// <summary>
    /// int RelationShip, int LastCleardQuest
    /// </summary>
    [Serializable]
    public struct TownInformation
    {
        public int RelationsShip;
        public int LastCleardQuest;
        //추가하기
        //public int IsProgress;
        //public int DisposalMonster;

        public TownInformation(int relations, int lastclearQuest)
        {
            RelationsShip = relations;
            LastCleardQuest = lastclearQuest;
        }
    }
    /// <summary>
    /// int PlayerLevel, flat PlayerExp, int PortionCount
    /// </summary>
    [Serializable]
    public struct PlayerInformation
    {
        public int PlayerLevel;
        public float PlayerExp;

        public PlayerInformation(int playerlevel, float playerexp)
        {

            PlayerLevel = playerlevel;
            PlayerExp = playerexp;    
        }
    }




    public class GameDataManager : Singleton<GameDataManager>
    {
        public PlayerInformation PlayerInfomation;
        public TownInformation AtownInformation;
        public TownInformation BtownInformation;
        public Dictionary<int, int> PlayerGamedata;
        public Dictionary<int, int> WareHouseGamedata;
        public int[] EquipmentItem;

        [SerializeField]
        ItemTable itemTable;

        /*NOTICE*/
        /* For Load String Data*/
        string[] playerInformationArray;
        string[] inventoryItemArray;
        string[] warehouseItemArray;
        string[] townInformationArray;
        string[] equipmentItemArray;

        void Start()
        {
            EquipmentItem = new int[3];
            PlayerGamedata = new Dictionary<int, int>();
            WareHouseGamedata = new Dictionary<int, int>();
            for (int i = 0; i < itemTable.sheets[0].list.Count; i++)
            {
                if (PlayerGamedata.ContainsKey(itemTable.sheets[0].list[i].Code))
                    PlayerGamedata[itemTable.sheets[0].list[i].Code] = 0;
                else
                    PlayerGamedata.Add(itemTable.sheets[0].list[i].Code, 0);

                if (WareHouseGamedata.ContainsKey(itemTable.sheets[0].list[i].Code))
                    WareHouseGamedata[itemTable.sheets[0].list[i].Code] = 0;
                else
                    WareHouseGamedata.Add(itemTable.sheets[0].list[i].Code, 0);

            }

            AtownInformation = new TownInformation(0, 0);
            BtownInformation = new TownInformation(0, 0);
            DontDestroyOnLoad(gameObject);
        }

        public void GetEquipmentStatus(ref int Attack,ref int Hp, ref int Defense)
        {
            for (int i = 0; i < itemTable.sheets[0].list.Count; i++)
            {
                if (EquipmentItem[0] == (itemTable.sheets[0].list[i].Code))
                {
                    Attack += itemTable.sheets[0].list[i].Attack;
                    Defense += itemTable.sheets[0].list[i].Defence;
                    Hp += itemTable.sheets[0].list[i].HP;
                }
                else if(EquipmentItem[1] == (itemTable.sheets[0].list[i].Code))
                {
                Attack += itemTable.sheets[0].list[i].Attack;
                Defense += itemTable.sheets[0].list[i].Defence;
                Hp += itemTable.sheets[0].list[i].HP;
                }
                else if (EquipmentItem[2] == (itemTable.sheets[0].list[i].Code))
                {
                    Attack += itemTable.sheets[0].list[i].Attack;
                    Defense += itemTable.sheets[0].list[i].Defence;
                    Hp += itemTable.sheets[0].list[i].HP;
                }
            }
        }
        
        public void SetGameDataToServer()
        {
            string playerLv = "";
            string playerExp = "";

            string AtownRelationship = "";
            string AtownQuest = "";
            string BtownRelationship = "";
            string BtownQuest = "";

            string InventoryItems = "";
            string WareHouseItems = "";
            string EquipmentItems = "";

            playerLv = PlayerInfomation.PlayerLevel.ToString();
            playerExp = PlayerInfomation.PlayerExp.ToString();
            AtownRelationship = AtownInformation.RelationsShip.ToString();
            AtownQuest = AtownInformation.LastCleardQuest.ToString();
            BtownRelationship = BtownInformation.RelationsShip.ToString();
            BtownQuest = BtownInformation.LastCleardQuest.ToString();
            
            for(int i=0;i<EquipmentItem.Length;i++)
            {
                EquipmentItems += EquipmentItem[i] + "/";
            }
               
            foreach (KeyValuePair<int, int> temp in PlayerGamedata)
            {
                string tempstring = temp.Key.ToString() + "_" + temp.Value.ToString() + "/";
                InventoryItems += tempstring;
            }

            foreach (KeyValuePair<int, int> temp in WareHouseGamedata)
            {
                string tempstring = temp.Key.ToString() + "_" + temp.Value.ToString() + "/";
                WareHouseItems += tempstring;
            }

            Dictionary<string, string> data = new Dictionary<string, string>();

            data.Add("PlayerInformation", playerLv + "/" + playerExp);
            data.Add("TownInformation", AtownRelationship + "/" + AtownQuest + "/" + BtownRelationship + "/" + BtownQuest);
            data.Add("InventroyItem", InventoryItems);
            data.Add("WareHouseItem", WareHouseItems);
            data.Add("EquipmentItem", EquipmentItems);

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
            string tempInventoryitems = "";
            string tempWarehouseitems = "";
            string tempTownInformations = "";
            string tempEquipmentItems = "";

            UserDataRecord userData = new UserDataRecord();
            AccountInfo.Instance.Info.UserData.TryGetValue("PlayerInformation", out userData);
            tempPlayerInformation = userData.Value;

            AccountInfo.Instance.Info.UserData.TryGetValue("InventroyItem", out userData);
            tempInventoryitems = userData.Value;

            AccountInfo.Instance.Info.UserData.TryGetValue("WareHouseItem", out userData);
            tempWarehouseitems = userData.Value;

            AccountInfo.Instance.Info.UserData.TryGetValue("TownInformation", out userData);
            tempTownInformations = userData.Value;

            AccountInfo.Instance.Info.UserData.TryGetValue("EquipmentItem", out userData);
            tempEquipmentItems = userData.Value;


            if (tempPlayerInformation != null)
                playerInformationArray = tempPlayerInformation.Split('/');

            //itemload
            if (tempInventoryitems != null)
            {
                inventoryItemArray = tempInventoryitems.Split('/');

                for (int i = 0; i < inventoryItemArray.Length - 1; i++)
                {
                    string[] tempArray = inventoryItemArray[i].Split('_');
                    if (PlayerGamedata.ContainsKey(Convert.ToInt32(tempArray[0])))
                        PlayerGamedata[Convert.ToInt32(tempArray[0])] = Convert.ToInt32(tempArray[1]);
                    else
                        PlayerGamedata.Add(Convert.ToInt32(tempArray[0]), Convert.ToInt32(tempArray[1]));



                }
            }

            if (tempWarehouseitems != null)
            {
                warehouseItemArray = tempWarehouseitems.Split('/');
                for (int i = 0; i < warehouseItemArray.Length - 1; i++)
                {
                    string[] tempArray = warehouseItemArray[i].Split('_');

                    if (PlayerGamedata.ContainsKey(Convert.ToInt32(tempArray[0])))
                        PlayerGamedata[Convert.ToInt32(tempArray[0])] = Convert.ToInt32(tempArray[1]);
                    else
                        WareHouseGamedata.Add(Convert.ToInt32(tempArray[0]), Convert.ToInt32(tempArray[1]));

                }
            }

            if(tempEquipmentItems!=null)
            {
                equipmentItemArray = tempEquipmentItems.Split('/');
                for(int i=0;i<equipmentItemArray.Length-1;i++)
                {
                    EquipmentItem[i] = Convert.ToInt32(equipmentItemArray[i]);
                }
            }




            //playerinfo load
            PlayerInfomation.PlayerLevel = Convert.ToInt32(playerInformationArray[0]);
            PlayerInfomation.PlayerExp =float.Parse(playerInformationArray[1]);




            if (tempTownInformations != null)
                townInformationArray = tempTownInformations.Split('/');
            //Town Relationship and quest load
            AtownInformation.RelationsShip = Convert.ToInt32(townInformationArray[0]);
            AtownInformation.LastCleardQuest = Convert.ToInt32(townInformationArray[1]);
            BtownInformation.RelationsShip = Convert.ToInt32(townInformationArray[2]);
            BtownInformation.LastCleardQuest = Convert.ToInt32(townInformationArray[3]);


        }



    }
}
