using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using System;

namespace ProjectB.UI.Presenter
{

    public class UIControllPresenter : MonoBehaviour
    {
        LoadType loadtype;
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
        GameObject storageButton;
        [SerializeField]
        GameObject combinationStoreButton;
        [SerializeField]
        GameObject tradeButton;
        [SerializeField]
        GameObject dungeonButton;
        [SerializeField]
        GameObject minimapUI;
        [SerializeField]
        GameObject winUI;
        [SerializeField]
        GameObject loseUI;
        [SerializeField]
        GameObject playerController;
        [SerializeField]
        GameObject playerHUD;

        bool isOpenedInventoryUI;
        bool isOpenedStorageUI;
        bool isOpenedCombinationUI;
        bool isOpenedTradeUI;
        bool isOpenedDungeonUI;
        bool isOpenedQuestUI;
        bool isOpenedMinimapUI;
        bool isOpenedWinUI;
        bool isOpenedLoseUI;


        private void Start()
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
        }
        public void OnClickedButton(int uiTypeNumber)
        {
            switch (uiTypeNumber)
            {
                case (int)UIType.Inventory:
                    isOpenedInventoryUI = true;
                    break;
                case (int)UIType.Storage:
                    isOpenedStorageUI = true;
                    break;
                case (int)UIType.CombinationStore:
                    isOpenedCombinationUI = true;
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
            SetActiveUI();
        }

        void EndStage(bool isWin)
        {
            if(isWin)
            {
                winUI.SetActive(false);
                loseUI.SetActive(true);
            }
            else
            {
                loseUI.SetActive(false);
                winUI.SetActive(true);
            }
        }

        void SetActiveUI()
        {
            try
            {
                if (loadtype == LoadType.Village)
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
                }
                else
                {
                    dungeonButton.SetActive(false);
                    storageButton.SetActive(false);
                    combinationStoreButton.SetActive(false);
                    tradeButton.SetActive(false);
                    minimapUI.SetActive(true);
                    playerController.SetActive(true);
                }
                playerHUD.SetActive(true);
                inventoryUI.SetActive(isOpenedInventoryUI);
                questUI.SetActive(isOpenedQuestUI);
            }
            catch
            {
                Debug.Log("필요한 UI가 장착되지 않았습니다.");
            }
  
        }

    }

}