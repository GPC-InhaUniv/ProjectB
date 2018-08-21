using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Item;
using ProjectB.GameManager;
public class EquipUIPresenter : MonoBehaviour {

    public void SwapToFromEquipWeaponSlotToInventorySlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;

        if (currentItem.ItemName == null || (currentItem.ItemType == ItemType.Equipmentable && swapItem.ItemType == ItemType.Equipmentable))
        {
            if((currentItem.Code % 100 <= 31 && swapItem.Code % 100 <= 11) && (swapItem.Code % 100 <= 31 && swapItem.Code % 100 >= 11))
            {
                currentItem.SetItem(swapItem.Code);
                swapItem.SetItem(SwapItemCode);
                currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                GameDataManager.Instance.EquipmentItem[0] = currentItem.Code;
            }
        }

    }

    public void SwapToFromEquipArmorSlotToInventorySlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;

        if (currentItem.ItemName == null || (currentItem.ItemType == ItemType.Equipmentable && swapItem.ItemType == ItemType.Equipmentable))
        {
            if ((currentItem.Code % 100 <= 33 && swapItem.Code % 100 <= 13) && (swapItem.Code % 100 <= 33 && swapItem.Code % 100 >= 13))
            {
                currentItem.SetItem(swapItem.Code);
                swapItem.SetItem(SwapItemCode);
                currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                GameDataManager.Instance.EquipmentItem[1] = currentItem.Code;
            }
        }
    }

    public void SwapToFromEquipHelmetSlotToInventorySlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;

        if (currentItem.ItemName == null || (currentItem.ItemType == ItemType.Equipmentable && swapItem.ItemType == ItemType.Equipmentable))
        {
            if ((currentItem.Code % 100 <= 32 && swapItem.Code % 100 <= 12) && (swapItem.Code % 100 <= 32 && swapItem.Code % 100 >= 12))
            {

                currentItem.SetItem(swapItem.Code);
                swapItem.SetItem(SwapItemCode);
                currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                GameDataManager.Instance.EquipmentItem[2] = currentItem.Code;
            }
        }
    }
}