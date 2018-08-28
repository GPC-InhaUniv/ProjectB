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
            if(swapItem.Code % 10 == 1)
            {
                currentItem.SetItem(swapItem.Code);
                currentItem.SetItemAmount(swapItem.ItemAmount);
                swapItem.SetItem(SwapItemCode);
                swapItem.SetItemAmount(SwapItemAmount);

                //currentItem.ItemNameText.text = currentItem.ItemName; // 삭제 예정
                currentItem.ItemImage.sprite = Test_AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.ItemName);
                //swapItem.ItemNameText.text = swapItem.ItemName; // 삭제 예정
                swapItem.ItemImage.sprite = Test_AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.ItemName);
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
            if (swapItem.Code % 10 == 3)
            {
                currentItem.SetItem(swapItem.Code);
                currentItem.SetItemAmount(swapItem.ItemAmount);
                swapItem.SetItem(SwapItemCode);
                swapItem.SetItemAmount(SwapItemAmount);

                //currentItem.ItemNameText.text = currentItem.ItemName; // 삭제 예정
                currentItem.ItemImage.sprite = Test_AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.ItemName);
                //swapItem.ItemNameText.text = swapItem.ItemName; // 삭제 예정
                swapItem.ItemImage.sprite = Test_AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.ItemName);
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
            if (swapItem.Code % 10 == 2)
            {
                currentItem.SetItem(swapItem.Code);
                currentItem.SetItemAmount(swapItem.ItemAmount);
                swapItem.SetItem(SwapItemCode);
                swapItem.SetItemAmount(SwapItemAmount);

                //currentItem.ItemNameText.text = currentItem.ItemName; // 삭제 예정
                currentItem.ItemImage.sprite = Test_AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.ItemName);
                //swapItem.ItemNameText.text = swapItem.ItemName; // 삭제 예정
                swapItem.ItemImage.sprite = Test_AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.ItemName);
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                GameDataManager.Instance.EquipmentItem[2] = currentItem.Code;
            }
        }
    }
}