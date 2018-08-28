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

        if (currentItem.ItemName != swapItem.ItemName)
        {
            if (swapItem.ItemType == ItemType.Equipmentable)
            {
                if (swapItem.Code % 10 == 1)
                {
                    if (swapItem.ItemAmount > 1)
                    {
                        if (currentItem.Code != 0)
                            GameDataManager.Instance.PlayerGamedata[currentItem.Code]++;
                        swapItem.DecreaseItemAmount();
                        currentItem.SetItem(swapItem.Code);
                        GameDataManager.Instance.PlayerGamedata[swapItem.Code]--;
                    }

                    else
                    {
                        currentItem.SetItem(swapItem.Code);
                        currentItem.SetItemAmount(swapItem.ItemAmount);
                        swapItem.SetItem(SwapItemCode);
                        swapItem.SetItemAmount(SwapItemAmount);
                        GameDataManager.Instance.PlayerGamedata[currentItem.Code]--;
                    }
                }
            }
        }
        currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
        swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
        swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
        GameDataManager.Instance.EquipmentItem[0] = currentItem.Code;
    }

    public void SwapToFromInventorySlotToEquipArmorSlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;
        int SwapItemAmount = currentItem.ItemAmount;

        if (currentItem.ItemName != swapItem.ItemName)
        {
            if (swapItem.ItemType == ItemType.Equipmentable)
            {
                if (swapItem.Code % 10 == 3)
                {
                    if (swapItem.ItemAmount > 1)
                    {
                        if (currentItem.Code != 0)
                            GameDataManager.Instance.PlayerGamedata[currentItem.Code]++;
                        swapItem.DecreaseItemAmount();
                        currentItem.SetItem(swapItem.Code);
                        GameDataManager.Instance.PlayerGamedata[swapItem.Code]--;
                    }

                    else
                    {
                        currentItem.SetItem(swapItem.Code);
                        currentItem.SetItemAmount(swapItem.ItemAmount);
                        swapItem.SetItem(SwapItemCode);
                        swapItem.SetItemAmount(SwapItemAmount);
                        GameDataManager.Instance.PlayerGamedata[currentItem.Code]--;
                    }
                }
            }
        }
        currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
        swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
        swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
        GameDataManager.Instance.EquipmentItem[1] = currentItem.Code;
    }

    public void SwapToFromInventorySlotToEquipHelmetSlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;
        int SwapItemAmount = currentItem.ItemAmount;

        if (currentItem.ItemName != swapItem.ItemName)
        {
            if (swapItem.ItemType == ItemType.Equipmentable)
            {
                if (swapItem.Code % 10 == 2)
                {
                    if (swapItem.ItemAmount > 1)
                    {
                        if(currentItem.Code != 0)
                            GameDataManager.Instance.PlayerGamedata[currentItem.Code]++;
                        swapItem.DecreaseItemAmount();
                        currentItem.SetItem(swapItem.Code);
                        GameDataManager.Instance.PlayerGamedata[swapItem.Code]--;
                    }

                    else
                    {
                        currentItem.SetItem(swapItem.Code);
                        currentItem.SetItemAmount(swapItem.ItemAmount);
                        swapItem.SetItem(SwapItemCode);
                        swapItem.SetItemAmount(SwapItemAmount);
                        GameDataManager.Instance.PlayerGamedata[currentItem.Code]--;
                        GameDataManager.Instance.PlayerGamedata[swapItem.Code]++;
                    }
                }
            }
        }
        currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
        swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
        swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
        GameDataManager.Instance.EquipmentItem[2] = currentItem.Code;
    }

    public void LoadToWearItem()
    {
        for(int i = 0; i < equipItem.Count; i++)
        {
            equipItem[i].SetItem(GameDataManager.Instance.EquipmentItem[i]);
        }
    }
}