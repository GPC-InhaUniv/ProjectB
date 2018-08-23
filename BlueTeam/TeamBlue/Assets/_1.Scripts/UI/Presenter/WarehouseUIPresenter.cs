using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Item;
using ProjectB.Inventory;

public class WarehouseUIPresenter : MonoBehaviour
{

    public void SwapToInventoryItem(Slot currentSlot, Slot swapSlot)
    {
        int SlotIndex;

        SlotIndex = currentSlot.transform.GetSiblingIndex();
        currentSlot.transform.SetSiblingIndex(swapSlot.transform.GetSiblingIndex());
        swapSlot.transform.SetSiblingIndex(SlotIndex);
    }

    public void SwapToFromInventorySlotToWarehouseSlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;
        int SwapItemAmount = currentItem.ItemAmount;

        currentItem.SetItem(swapItem.Code);
        currentItem.SetItemAmount(swapItem.ItemAmount);
        swapItem.SetItem(SwapItemCode);
        swapItem.SetItemAmount(SwapItemAmount);
        currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
        currentItem.ItemNameText.text = currentItem.ItemName;
        swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
        swapItem.ItemNameText.text = swapItem.ItemName;
    }
}
