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
    [SerializeField] List<Item> combinationResourcesItems = new List<Item>();
    int[] requirematerials;
    int combinationItemCode;
    public static AddItemDelegate addItemDelegate;

    private void Awake()
    {
        requirematerials = new int[4];
    }

    public void SwapToFromInventorySlotToCombinationSlot(Item currentItem, Item swapItem)
    {
        int SwapItemCode = currentItem.Code;
        if (currentItem.ItemName == null || (currentItem.ItemType == ItemType.Exapandable && swapItem.ItemType == ItemType.Exapandable))
        {
            if ((currentItem.Code % 100 <= 33 && swapItem.Code % 100 <= 33) && (swapItem.Code % 100 <= 33 && swapItem.Code % 100 >= 11))
            {
                currentItem.SetItem(swapItem.Code);
                swapItem.SetItem(SwapItemCode);
                currentItem.ItemAmountText.text = currentItem.ItemAmount.ToString();
                swapItem.ItemAmountText.text = swapItem.ItemAmount.ToString();
                DisplayToCombinationResourcesSlot(currentItem);
                combinationItemCode = currentItem.Code;
            }
        }
    }

    public void OnClickCombinationButton()
    {
        bool checkCombination = true;
        for(int i=0;i<4;i++)
        {
            if (GameDataManager.Instance.PlayerGamedata[i + 3000] < requirematerials[i])
            {
                checkCombination = false;
                break;
            }
        }

        if(checkCombination == true)
        {
            GameDataManager.Instance.PlayerGamedata[combinationItemCode + 1000]++;
            addItemDelegate();
        }
    }

    public void DisplayToCombinationResourcesSlot(Item item)
    {
        combinationResourcesItems[0].ItemAmountText.text = item.RecipeBrick.ToString();
        combinationResourcesItems[1].ItemAmountText.text = item.RecipeWood.ToString();
        combinationResourcesItems[2].ItemAmountText.text = item.RecipeIron.ToString();
        combinationResourcesItems[3].ItemAmountText.text = item.RecipeSheep.ToString();

        requirematerials[0] = item.RecipeBrick;
        requirematerials[1] = item.RecipeWood;
        requirematerials[2] = item.RecipeIron;
        requirematerials[3] = item.RecipeSheep;
    }
}
