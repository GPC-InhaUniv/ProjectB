using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using ProjectB.Item;
using ProjectB.Inventory;
using UnityEngine.UI;
using System.Linq;

public delegate void InitializeCombinationResourcesSlot();

public class InventoryUIPresenter : MonoBehaviour
{
    [SerializeField] List<Item> items = new List<Item>();
    public List<Item> Items { get { return items; } }
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

    //public void AddItem_Test(int code)
    //{
    //    for (int i = 0; i < items.Count; i++)
    //    {
    //        if (items[i].Code == 0)
    //        {
    //            items[i].SetItem(code);
    //            items[i].IncreaseItemAmount();
    //            items[i].ItemAmountText.text = items[i].ItemAmount.ToString();
    //        }

    //        else if (items[i].Code == code)
    //        {
    //            if (items[i].ItemType != ItemType.Equipmentable)
    //            {
    //                items[i].IncreaseItemAmount();
    //                items[i].ItemAmountText.text = items[i].ItemAmount.ToString();
    //            }

    //            else
    //                items[i].SetItem(code);
    //        }

    //        else
    //        {
    //            continue;
    //        }

    //        break;
    //    }
    //}

    public void AddItem()
    {

        for (int j = 0; j < GameDataManager.Instance.PlayerGamedata.Count; j++)
        {

            int tempKey = GameDataManager.Instance.PlayerGamedata.Keys.ToList()[j];
            int tempValue= GameDataManager.Instance.PlayerGamedata.Values.ToList()[j];

            if (tempValue == 0)
                continue;

            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetItemAmount(tempValue);

                if (items[i].ItemAmount == 0)
                    items[i].SetItem(0);

                if (items[i].Code == 0)
                {
                    items[i].SetItem(tempKey);
                    items[i].IncreaseItemAmount();
                }

                else if (items[i].Code == tempKey)
                    items[i].IncreaseItemAmount();

                else if (items[i].ItemAmount == 0)
                    items[i].SetItem(0);

                else if (items[i].Code != tempKey)
                    continue;

                items[i].SetItemAmount(tempValue);
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
        int SwapItemCode = currentItem.Code;
        int SwapItemAmount = currentItem.ItemAmount;

        if (currentItem.ItemName == null)
        {
            currentItem.SetItem(swapItem.Code);
            currentItem.SetItemAmount(swapItem.ItemAmount);
            swapItem.SetItem(SwapItemCode);
            swapItem.SetItemAmount(SwapItemAmount);

            currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
            currentItem.ItemNameText.text = currentItem.ItemName;
            swapItem.ItemNameText.text = swapItem.ItemName;
            initializeCombinationResourcesSlot();
        }
    }

    public void SwapToFromEquipSlotToInventorySlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;
        int SwapItemAmount = currentItem.ItemAmount;

        if (currentItem.ItemName == null)
        {
            currentItem.SetItem(swapItem.Code);
            currentItem.SetItemAmount(swapItem.ItemAmount);
            swapItem.SetItem(SwapItemCode);
            swapItem.SetItemAmount(SwapItemAmount);

            currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
            currentItem.ItemNameText.text = currentItem.ItemName;
            swapItem.ItemNameText.text = swapItem.ItemName;
            for (int i = 0; i < GameDataManager.Instance.EquipmentItem.Length; i++)
            {

                if (GameDataManager.Instance.EquipmentItem[i] != 0)
                {
                    if (GameDataManager.Instance.EquipmentItem[i] == currentItem.Code)
                        GameDataManager.Instance.EquipmentItem[i] = 0;
                }
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
}
