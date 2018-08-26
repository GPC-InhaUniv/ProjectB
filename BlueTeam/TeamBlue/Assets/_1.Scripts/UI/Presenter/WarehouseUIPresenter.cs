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
        currentItem.ItemNameText.text = currentItem.ItemName; // 삭제 예정
        //currentItem.ItemImage.sprite = 에셋번들 로드 이미지
        swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
        swapItem.ItemNameText.text = swapItem.ItemName; // 삭제 예정
        //swapItem.ItemImage.sprite = 에셋번들 로드 이미지
    }
}
