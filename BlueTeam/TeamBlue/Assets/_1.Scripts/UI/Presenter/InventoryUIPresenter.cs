using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using ProjectB.Item;
using ProjectB.Inventory;
using UnityEngine.UI;

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
        foreach (KeyValuePair<int, int> temp in GameDataManager.Instance.PlayerGamedata)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (temp.Value == 0)
                {
                    items[i].InitializationItem();
                    items[i].ItemAmountText.text = items[i].ItemAmount.ToString();
                }

                if (items[i].Code == 0)
                {
                    items[i].SetItem(temp.Key);
                    items[i].IncreaseItemAmount();
                }

                else if (items[i].Code == temp.Key)
                    items[i].IncreaseItemAmount();

                else if (items[i].Code != temp.Key)
                    continue;

                items[i].SetItemAmount(temp.Value);
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
