using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.UI
{

    public class VillageMenuPresenter : MonoBehaviour
    {
        
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

        bool isOpenedInventoryUI;
        bool isOpenedStorageUI;
        bool isOpenedCombinationUI;
        bool isOpenedTradeUI;
        bool isOpenedDungeonUI;
        bool isOpenedQuestUI;

        bool isInstalledUI;

        private void Start()
        {
            CheckEquipUI();
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

        void CheckEquipUI()
        {
            if (inventoryUI == null)
            {
                return;
            }
            else if (storageUI == null)
            {
                return;
            }
            else if (combinationStoreUI == null)
            {
                return;
            }
            else if (tradeUI == null)
            {
                return;
            }
            else if (dungeonUI == null)
            {
                return;
            }
            else if (questUI == null)
            {
                return;
            }
            else
            {
                isInstalledUI = true;
            }
                
        }

        void SetActiveUI()
        {
            if (isInstalledUI)
            {
                inventoryUI.SetActive(isOpenedInventoryUI);
                storageUI.SetActive(isOpenedStorageUI);
                combinationStoreUI.SetActive(isOpenedCombinationUI);
                tradeUI.SetActive(isOpenedTradeUI);
                dungeonUI.SetActive(isOpenedDungeonUI);
                questUI.SetActive(isOpenedQuestUI);
            }
            else
            {
                Debug.Log("필요한 UI가 장착되지 않았습니다.");
            }
        }

    }

}