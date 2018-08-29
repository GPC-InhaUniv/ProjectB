using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using ProjectB.Item;
using ProjectB.GameManager;

public class EquipUIPresenter : MonoBehaviour
{
    [SerializeField] GameObject equipPanel;
    [SerializeField] List<Item> equipItems;
    // index 0 : HP, 1 : Attack, 2 : Defence 
    [SerializeField] Text[] helmetInfos;
    [SerializeField] Text[] amorInfos;
    [SerializeField] Text[] weaponInfos;

    public void Awake()
    {
        LoadToWearItem();
        InventoryUIPresenter.loadToWearItem += LoadToWearItem;
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
                        if(SwapItemCode != 0)
                            GameDataManager.Instance.PlayerGamedata[swapItem.Code]++;
                    }
                }
            }
        }
        currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
        swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
        swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
        GameDataManager.Instance.EquipmentItem[0] = currentItem.Code;

        weaponInfos[0].text = "HP : " + currentItem.Hp.ToString();
        weaponInfos[1].text = "Attack : " + currentItem.Attack.ToString();
        weaponInfos[2].text = "Defence : " + currentItem.Defence.ToString();
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
                        if (SwapItemCode != 0)
                            GameDataManager.Instance.PlayerGamedata[swapItem.Code]++;
                    }
                }
            }
        }
        currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
        swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
        swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
        GameDataManager.Instance.EquipmentItem[1] = currentItem.Code;

        amorInfos[0].text = "HP : " + currentItem.Hp.ToString();
        amorInfos[1].text = "Attack : " + currentItem.Attack.ToString();
        amorInfos[2].text = "Defence : " + currentItem.Defence.ToString();
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
                        if (SwapItemCode != 0)
                            GameDataManager.Instance.PlayerGamedata[swapItem.Code]++;
                    }
                }
            }
        }
        currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
        swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);
        swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
        GameDataManager.Instance.EquipmentItem[2] = currentItem.Code;

        helmetInfos[0].text = "HP : " + currentItem.Hp.ToString();
        helmetInfos[1].text = "Attack : " + currentItem.Attack.ToString();
        helmetInfos[2].text = "Defence : " + currentItem.Defence.ToString();
    }

    public void OnClickEquipExitButton()
    {
        equipPanel.SetActive(false);
    }

    public void LoadToWearItem()
    {
        for(int i = 0; i < equipItems.Count; i++)
        {
            equipItems[i].SetItem(GameDataManager.Instance.EquipmentItem[i]);
        }

        weaponInfos[0].text = "HP : " + equipItems[0].Hp.ToString();
        weaponInfos[1].text = "Attack : " + equipItems[0].Attack.ToString();
        weaponInfos[2].text = "Defence : " + equipItems[0].Defence.ToString();

        amorInfos[0].text = "HP : " + equipItems[1].Hp.ToString();
        amorInfos[1].text = "Attack : " + equipItems[1].Attack.ToString();
        amorInfos[2].text = "Defence : " + equipItems[1].Defence.ToString();

        helmetInfos[0].text = "HP : " + equipItems[2].Hp.ToString();
        helmetInfos[1].text = "Attack : " + equipItems[2].Attack.ToString();
        helmetInfos[2].text = "Defence : " + equipItems[2].Defence.ToString();
    }
}