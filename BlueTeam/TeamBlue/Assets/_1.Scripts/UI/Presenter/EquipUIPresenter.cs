using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Item;
using ProjectB.GameManager;
public class EquipUIPresenter : MonoBehaviour {

    public void SwapToFromInventorySlotToEquipWeaponSlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;
        int SwapItemAmount = currentItem.ItemAmount;

        if (swapItem.ItemType == ItemType.Equipmentable)
        {
<<<<<<< HEAD
            if(swapItem.Code % 10 == 1)
=======
            if((currentItem.Code % 100 <= 31 && swapItem.Code % 100 >= 11) && (swapItem.Code % 100 <= 31 && swapItem.Code % 100 >= 11))
>>>>>>> 577de2a63bd2fc72aae4d678cbe548b7e93868e7
            {
                currentItem.SetItem(swapItem.Code);
                currentItem.SetItemAmount(swapItem.ItemAmount);
                swapItem.SetItem(SwapItemCode);
                swapItem.SetItemAmount(SwapItemAmount);

                currentItem.ItemNameText.text = currentItem.ItemName;
                swapItem.ItemNameText.text = swapItem.ItemName;
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                GameDataManager.Instance.EquipmentItem[0] = currentItem.Code;
            }
        }

    }

    public void SwapToFromInventorySlotToEquipArmorSlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;
        int SwapItemAmount = currentItem.ItemAmount;

        if (swapItem.ItemType == ItemType.Equipmentable)
        {
<<<<<<< HEAD
            if (swapItem.Code % 10 == 3)
=======
            if ((currentItem.Code % 100 <= 33 && swapItem.Code % 100 >= 13) && (swapItem.Code % 100 <= 33 && swapItem.Code % 100 >= 13))
>>>>>>> 577de2a63bd2fc72aae4d678cbe548b7e93868e7
            {
                currentItem.SetItem(swapItem.Code);
                currentItem.SetItemAmount(swapItem.ItemAmount);
                swapItem.SetItem(SwapItemCode);
                swapItem.SetItemAmount(SwapItemAmount);

                currentItem.ItemNameText.text = currentItem.ItemName;
                swapItem.ItemNameText.text = swapItem.ItemName;
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                GameDataManager.Instance.EquipmentItem[1] = currentItem.Code;
            }
        }
    }

    public void SwapToFromInventorySlotToEquipHelmetSlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;
        int SwapItemAmount = currentItem.ItemAmount;

        if (swapItem.ItemType == ItemType.Equipmentable)
        {
<<<<<<< HEAD
            if (swapItem.Code % 10 == 2)
=======
            if ((currentItem.Code % 100 <= 32 && swapItem.Code % 100 >= 12) && (swapItem.Code % 100 <= 32 && swapItem.Code % 100 >= 12))
>>>>>>> 577de2a63bd2fc72aae4d678cbe548b7e93868e7
            {
                currentItem.SetItem(swapItem.Code);
                currentItem.SetItemAmount(swapItem.ItemAmount);
                swapItem.SetItem(SwapItemCode);
                swapItem.SetItemAmount(SwapItemAmount);

                currentItem.ItemNameText.text = currentItem.ItemName;
                swapItem.ItemNameText.text = swapItem.ItemName;
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                GameDataManager.Instance.EquipmentItem[2] = currentItem.Code;
            }
        }
    }
}