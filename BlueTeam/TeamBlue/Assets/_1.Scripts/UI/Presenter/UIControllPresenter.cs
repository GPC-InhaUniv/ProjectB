using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using System;

namespace ProjectB.UI.Presenter
{

    public class UIControllPresenter : MonoBehaviour , IExitable
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
        [SerializeField]
        GameObject villageSetUpUI;
        [SerializeField]
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

        bool isOpenedEquipmentUI;

        private void OnEnable()
        {
            loadtype = GameControllManager.Instance.CurrentLoadType;
            OnClickedBackGround();
            SetActiveUI();
        }
        private void OnDisable()
        {
            isOpenedDungeonUI = false;
        }
        enum UIType
        {
            Inventory,
            Storage,
            CombinationStore,
            Trade,
            Dungeon,
            Quest,
     
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
           
            isOpenedEquipmentUI = false;
            isOpenedWinUI = false;

            SetActiveUI();
        }

        public void EndStage()
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
                    
                    villageSetUpUI.SetActive(true);
                    dungeonSetUpUI.SetActive(false);
                  
                }
                else
                {
                    dungeonButton.SetActive(false);
                    storageButton.SetActive(false);
                    combinationStoreButton.SetActive(false);
                    tradeButton.SetActive(false);
                    minimapUI.SetActive(true);
                    playerController.SetActive(true);
                    dungeonSetUpUI.SetActive(true);
                    villageSetUpUI.SetActive(false);

                }
                playerHUD.SetActive(true);
                inventoryUI.SetActive(isOpenedInventoryUI);
                questUI.SetActive(isOpenedQuestUI);
                equipmentUI.SetActive(isOpenedEquipmentUI);
                dungeonUI.SetActive(isOpenedDungeonUI);
            }
            catch
            {
                Debug.Log("필요한 UI가 장착되지 않았습니다.");
            }
  
        }

    }

}