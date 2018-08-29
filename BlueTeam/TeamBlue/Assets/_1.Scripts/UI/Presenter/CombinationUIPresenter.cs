using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.GameManager;
using ProjectB.Item;
using System;

public delegate void AddItemDelegate();


public class CombinationUIPresenter : MonoBehaviour
{
    [SerializeField] Item combinationSlotItem;
    [SerializeField] List<Item> combinationResourcesItems = new List<Item>();
    int[] requirematerials;
    int combinationItemCode;
    public static AddItemDelegate addItemDelegate;

    private void Awake()
    {
        InventoryUIPresenter.initializeCombinationResourcesSlot += InitializeToCombinationResourcesSlot;
        requirematerials = new int[4];
    }

    private void OnDisable()
    {
        ResetCombinationSlot();
    }

    public void SwapToFromInventorySlotToCombinationSlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;
        int SwapItemAmount = currentItem.ItemAmount;

        if (swapItem.ItemType == ItemType.Exapandable)
        {
            if (swapItem.Code % 100 >= 11 && swapItem.Code % 100 <= 33)
            {
                currentItem.SetItem(swapItem.Code);
                currentItem.SetItemAmount(swapItem.ItemAmount);
                swapItem.SetItem(SwapItemCode);
                swapItem.SetItemAmount(SwapItemAmount);

                currentItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, currentItem.Image);
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                swapItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, swapItem.Image);

                DisplayToCombinationResourcesSlot(currentItem);
                combinationItemCode = currentItem.Code;
            }
        }
    }

    public void OnClickCombinationButton()
    {
        if (combinationItemCode == 0)
            return;

        bool checkCombination = true;
        for(int i = 0; i < 4; i++)
        {
            if (GameDataManager.Instance.PlayerGamedata[i + 3000] < requirematerials[i])
            {
                checkCombination = false;
                break;
            }
        }

        if(checkCombination == true)
        {
            for (int i = 0; i < 4; i++)
            {
                GameDataManager.Instance.PlayerGamedata[i + 3000] -= requirematerials[i];
            }
            GameDataManager.Instance.PlayerGamedata[combinationItemCode]--;

            if (GameDataManager.Instance.PlayerGamedata[combinationItemCode] <= 0)
                GameDataManager.Instance.PlayerGamedata[combinationItemCode] = 0;

            GameDataManager.Instance.PlayerGamedata[combinationItemCode + 1000]++;
            GameDataManager.Instance.SetGameDataToServer();
            ResetCombinationSlot();
            addItemDelegate();
        }
    }

    public void DisplayToCombinationResourcesSlot(Item item)
    {
        combinationResourcesItems[0].ItemAmountText.text = item.RecipeBrick.ToString();
        combinationResourcesItems[1].ItemAmountText.text = item.RecipeWood.ToString();
        combinationResourcesItems[2].ItemAmountText.text = item.RecipeIron.ToString();
        combinationResourcesItems[3].ItemAmountText.text = item.RecipeSheep.ToString();

        combinationResourcesItems[0].ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, "Brick");
        combinationResourcesItems[1].ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, "Wood");
        combinationResourcesItems[2].ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, "Iron");
        combinationResourcesItems[3].ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, "Sheep");

        requirematerials[0] = item.RecipeBrick;
        requirematerials[1] = item.RecipeWood;
        requirematerials[2] = item.RecipeIron;
        requirematerials[3] = item.RecipeSheep;
    }

    public void InitializeToCombinationResourcesSlot()
    {
        for(int i = 0; i < requirematerials.Length; i++)
        {
            combinationResourcesItems[i].ItemAmountText.text = 0.ToString();
            requirematerials[i] = 0;
        }
    }

    public void ResetCombinationSlot()
    {
        combinationSlotItem.InitializationItem();
        combinationSlotItem.ItemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, combinationSlotItem.Image);
        InitializeToCombinationResourcesSlot();
    }
}
