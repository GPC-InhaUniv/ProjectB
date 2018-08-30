using UnityEngine;
using ProjectB.GameManager;
using ProjectB.Utility;

namespace ProjectB.UI.Presenter
{

    public class UIControllPresenter : MonoBehaviour, IExitable
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

        [Space, Header("Button")]
        [SerializeField]
        GameObject storageButton;
        [SerializeField]
        GameObject combinationStoreButton;
        [SerializeField]
        GameObject tradeButton;
        [SerializeField]
        GameObject dungeonButton;


        bool isOpenedTradeUI;
        bool isOpenedDungeonUI;
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
                    inventoryUI.SetActive(true);
                    equipmentUI.SetActive(true);
                    break;
                case (int)UIType.Storage:
                    //창고 미구현
                  //  storageUI.SetActive(true);
                    break;
                case (int)UIType.CombinationStore:
                    combinationStoreUI.SetActive(true);
                    inventoryUI.SetActive(true);
                    break;
                case (int)UIType.Trade:
                    isOpenedTradeUI = true;
                    break;
                case (int)UIType.Dungeon:
                    isOpenedDungeonUI = true;
                    isOpenedTradeUI = false;
                    break;
                case (int)UIType.Quest:
                    questUI.SetActive(true);
                    break;
            }
            SetActiveUI();
        }


        public void OnClickedBackGround()
        {
            isOpenedTradeUI = false;
            SetActiveUI();
        }
        public void EndStage()
        {
            resultUI.SetActive(true);
        }
        void SetActiveUI()
        {
            if (loadtype == LoadType.Village || loadtype == LoadType.VillageCheckDownLoad)
            {
                storageButton.SetActive(true);
                combinationStoreButton.SetActive(true);
                tradeButton.SetActive(true);
                dungeonButton.SetActive(true);
                minimapUI.SetActive(false);
                playerController.SetActive(false);
                
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
            dungeonUI.SetActive(isOpenedDungeonUI);
            tradeUI.SetActive(isOpenedTradeUI);
        }
    }

}
