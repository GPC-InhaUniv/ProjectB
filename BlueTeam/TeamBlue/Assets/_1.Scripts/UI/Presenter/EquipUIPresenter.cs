using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Item;
using ProjectB.GameManager;
public class EquipUIPresenter : MonoBehaviour
{
    [SerializeField] List<Item> equipItem;

    public void Awake()
    {
        LoadToWearItem();
    }

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

                currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
                swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
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

                currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
                swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
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

                currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
                swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                GameDataManager.Instance.EquipmentItem[2] = currentItem.Code;
            }
        }
    }

    public void LoadToWearItem()
    {
        equipItem[0].SetItem(GameDataManager.Instance.EquipmentItem[2]);
        equipItem[1].SetItem(GameDataManager.Instance.EquipmentItem[1]);
        equipItem[2].SetItem(GameDataManager.Instance.EquipmentItem[0]);
    }
}