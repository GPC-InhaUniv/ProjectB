﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using System;

namespace ProjectB.UI.Presenter
{

    public class UIControllPresenter : MonoBehaviour
    {
        LoadType loadtype;
        [Header("Panel")]
        [SerializeField, Tooltip("UIType Number : 0")]
        GameObject inventoryUI;
        [SerializeField, Tooltip("UIType Number : 1")]
        GameObject storageUI;
        [SerializeField, Tooltip("UIType Number : 2")]
        GameObject combinationStoreUI;
        [SerializeField, Tooltip("UIType Number : 3")]
        GameObject tradeUI;
        [SerializeField, Tooltip("UIType Number : 4")]
        GameObject dungeonUI;
        [SerializeField, Tooltip("UIType Number : 5")]
        GameObject questUI;
        [SerializeField, Tooltip("UIType Number : 6")]
        GameObject villageSetUpUI;
        [SerializeField, Tooltip("UIType Number : 6")]
        GameObject dungeonSetUpUI;
        [SerializeField]
        GameObject equipmentUI;
        [SerializeField]
        GameObject minimapUI;
        [SerializeField]
        GameObject resultUI;
        [SerializeField]
        GameObject playerController;
        [SerializeField]
        GameObject playerHUD;
        [Space,Header("Button")]
        
        [SerializeField]
        GameObject storageButton;
        [SerializeField]
        GameObject combinationStoreButton;
        [SerializeField]
        GameObject tradeButton;
        [SerializeField]
        GameObject dungeonButton;
        

        bool isOpenedInventoryUI;
        bool isOpenedStorageUI;
        bool isOpenedCombinationUI;
        bool isOpenedTradeUI;
        bool isOpenedDungeonUI;
        bool isOpenedQuestUI;
        bool isOpenedMinimapUI;
        bool isOpenedWinUI;
        bool isOpenedLoseUI;
        bool isOpenedSetupUI;
        bool isOpenedEquipmentUI;

        private void OnEnable()
        {
            loadtype = GameControllManager.Instance.CurrentLoadType;
            SetActiveUI();
        }    

        enum UIType
        {
            Inventory,
            Storage,
            CombinationStore,
            Trade,
            Dungeon,
            Quest,
            SetUp,
        }
        public void OnClickedButton(int uiTypeNumber)
        {
            switch (uiTypeNumber)
            {
                case (int)UIType.Inventory:
                    isOpenedInventoryUI = true;
                    isOpenedEquipmentUI = true;
                    break;
                case (int)UIType.Storage:
                    isOpenedStorageUI = true;
                    break;
                case (int)UIType.CombinationStore:
                    isOpenedCombinationUI = true;
                    isOpenedInventoryUI = true;
                    break;
                case (int)UIType.Trade:
                    isOpenedTradeUI = true;
                    break;
                case (int)UIType.Dungeon:
                    isOpenedDungeonUI = true;
                    break;
                case (int)UIType.Quest:
                    isOpenedQuestUI = true;
                    break;
                case (int)UIType.SetUp:
                       isOpenedSetupUI= true;
                        break;
            }
            SetActiveUI();
        }


        public void OnClickedBackGround()
        {
            isOpenedInventoryUI = false;
            isOpenedStorageUI = false;
            isOpenedCombinationUI = false;
            isOpenedTradeUI = false;
            isOpenedDungeonUI = false;
            isOpenedQuestUI = false;
            isOpenedSetupUI = false;
            isOpenedEquipmentUI = false;
            SetActiveUI();
        }

        void EndStage()
        {
            resultUI.SetActive(true);
        }

        void SetActiveUI()
        {
            try
            {
                if (loadtype == LoadType.Village || loadtype == LoadType.VillageCheckDownLoad)
                {
                    storageButton.SetActive(true);
                    combinationStoreButton.SetActive(true);
                    tradeButton.SetActive(true);
                    dungeonButton.SetActive(true);
                    minimapUI.SetActive(false);
                    playerController.SetActive(false);
                    storageUI.SetActive(isOpenedStorageUI);
                    combinationStoreUI.SetActive(isOpenedCombinationUI);
                    tradeUI.SetActive(isOpenedTradeUI);
                    dungeonUI.SetActive(isOpenedDungeonUI);
                    villageSetUpUI.SetActive(isOpenedSetupUI);
                  
                }
                else
                {
                    dungeonButton.SetActive(false);
                    storageButton.SetActive(false);
                    combinationStoreButton.SetActive(false);
                    tradeButton.SetActive(false);
                    minimapUI.SetActive(true);
                    playerController.SetActive(true);
                    dungeonSetUpUI.SetActive(isOpenedSetupUI);
                  
                }
                playerHUD.SetActive(true);
                inventoryUI.SetActive(isOpenedInventoryUI);
                questUI.SetActive(isOpenedQuestUI);
                equipmentUI.SetActive(isOpenedEquipmentUI);
            }
            catch
            {
                Debug.Log("필요한 UI가 장착되지 않았습니다.");
            }
  
        }

    }

}