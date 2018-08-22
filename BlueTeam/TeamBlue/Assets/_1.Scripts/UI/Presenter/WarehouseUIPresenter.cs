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

    public void SwapToFromCombinationSlotToInventorySlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;

        currentItem.SetItem(swapItem.Code);
        swapItem.SetItem(SwapItemCode);
        currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
        swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
    }
}
