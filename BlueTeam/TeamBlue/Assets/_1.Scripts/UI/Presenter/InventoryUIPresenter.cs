using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using ProjectB.Item;
using ProjectB.Inventory;
using System.Linq;

public delegate void InitializeCombinationResourcesSlot();

public class InventoryUIPresenter : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] List<Item> items;
    public static InitializeCombinationResourcesSlot initializeCombinationResourcesSlot;

    private void OnEnable()
    {
        AddItem();
    }

    private void OnDisable()
    {
        ResetItems();
        GameDataManager.Instance.SetGameDataToServer();
    }

    private void Awake()
    {
      
        CombinationUIPresenter.addItemDelegate += AddItem;
    }

    public void AddItem()
    {
        for (int j = 0; j < GameDataManager.Instance.PlayerGamedata.Count; j++)
        {
            int tempKey = GameDataManager.Instance.PlayerGamedata.Keys.ToList()[j];
            int tempValue= GameDataManager.Instance.PlayerGamedata.Values.ToList()[j];

            if (tempValue == 0)
            {
                items[j].SetItem(0);
                continue;
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Code == 0)
                {
                    items[i].SetItem(tempKey);
                    items[i].IncreaseItemAmount();
                }

                else if (items[i].Code == tempKey)
                    items[i].IncreaseItemAmount();

                else if (items[i].Code != tempKey)
                    continue;

                items[i].SetItemAmount(tempValue);
                items[i].ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, items[i].Image);
                items[i].ItemAmountText.text = items[i].ItemAmount.ToString();
                break;
            }
        }
    }

    public void SwapToInventoryItem(Slot currentSlot, Slot swapSlot)
    {
        int SlotIndex;

        SlotIndex = currentSlot.transform.GetSiblingIndex();
        currentSlot.transform.SetSiblingIndex(swapSlot.transform.GetSiblingIndex());
        swapSlot.transform.SetSiblingIndex(SlotIndex);
    }

    public void SwapToFromCombinationSlotToInventorySlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = swapItem.Code;
        int SwapItemAmount = swapItem.ItemAmount;

        if (currentItem.ItemName == null)
        {
            swapItem.SetItem(currentItem.Code);
            swapItem.SetItemAmount(currentItem.ItemAmount);
            currentItem.SetItem(SwapItemCode);
            currentItem.SetItemAmount(SwapItemAmount);

            currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
            currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
            swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
            initializeCombinationResourcesSlot();
        }
    }

    public void SwapToFromEquipSlotToInventorySlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = swapItem.Code;
        int SwapItemAmount = swapItem.ItemAmount;

        if(currentItem.ItemName == swapItem.ItemName)
        {
            currentItem.IncreaseItemAmount();
            swapItem.SetItem(0);
            GameDataManager.Instance.PlayerGamedata[currentItem.Code]++;
        }

        else if (currentItem.ItemName == null)
        {
            swapItem.SetItem(currentItem.Code);
            swapItem.SetItemAmount(currentItem.ItemAmount);
            currentItem.SetItem(SwapItemCode);
            currentItem.SetItemAmount(SwapItemAmount);
            GameDataManager.Instance.PlayerGamedata[currentItem.Code]++;
        }

        currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
        currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
        swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
        for (int i = 0; i < GameDataManager.Instance.EquipmentItem.Length; i++)
        {
            if (GameDataManager.Instance.EquipmentItem[i] != 0)
            {
                if (GameDataManager.Instance.EquipmentItem[i] == currentItem.Code)
                    GameDataManager.Instance.EquipmentItem[i] = 0;
            }
        }
    }

    public void ResetItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].InitializationItem();
            items[i].ItemAmountText.text = items[i].ItemAmount.ToString();
        }
    }

    public void OnClickInventoryExitButton()
    {
        inventoryPanel.SetActive(false);
    }
}
