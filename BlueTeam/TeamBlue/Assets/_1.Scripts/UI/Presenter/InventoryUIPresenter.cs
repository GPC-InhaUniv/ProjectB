using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;
using ProjectB.Item;
using ProjectB.Inventory;

public class InventoryUIPresenter : MonoBehaviour
{
    [SerializeField] List<Item> items = new List<Item>();
    public List<Item> Items { get { return items; } }

    private void Awake()
    {
        AddItem();
        CombinationUIPresenter.addItemDelegate += AddItem;
    }

    public void AddItem()
    {
        foreach(KeyValuePair<int,int> temp in GameDataManager.Instance.PlayerGamedata)
        {
            if (temp.Value == 0)
                continue;

            else
            {
                for(int i = 0; i < items.Count; i++)
                {
                    if (items[i].Code == temp.Key)
                    {
                        if (items[i].ItemType != ItemType.Equipmentable)
                            items[i].IncreaseItemAmount();

                        else
                            items[i].SetItem(temp.Value);
                    }
                    else if (items[i].Code == 0)
                    {
                        items[i].SetItem(temp.Key);
                        items[i].IncreaseItemAmount();
                        items[i].ItemAmountText.text = items[i].ItemAmount.ToString();
                    }
                    else
                        continue;

                    break;
                }
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
        if (currentItem.ItemName == null || (currentItem.ItemType == ItemType.Exapandable && swapItem.ItemType == ItemType.Exapandable))
        {
            if (currentItem.ItemName == null || (currentItem.Code % 100 <= 33 && currentItem.Code % 100 >= 11) && (swapItem.Code % 100 <= 33 && swapItem.Code % 100 >= 11))
            {
                currentItem.SetItem(swapItem.Code);
                swapItem.SetItem(SwapItemCode);
                currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
            }
        }
    }

    public void SwapToFromEquipSlotToInventorySlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;

        if(currentItem.ItemName == null || (currentItem.ItemType == ItemType.Equipmentable && swapItem.ItemType == ItemType.Equipmentable))
        {
            currentItem.SetItem(swapItem.Code);
            swapItem.SetItem(SwapItemCode);
            currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
            swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
            for (int i=0;i<GameDataManager.Instance.EquipmentItem.Length;i++)
            {

                if (GameDataManager.Instance.EquipmentItem[i]!=0)
                {
                    if (GameDataManager.Instance.EquipmentItem[i] == currentItem.Code)
                        GameDataManager.Instance.EquipmentItem[i] = 0;
                }
            }
        }
    }


}
