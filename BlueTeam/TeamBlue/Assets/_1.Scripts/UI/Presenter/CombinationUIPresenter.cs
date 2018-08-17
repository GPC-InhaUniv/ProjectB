using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.GameManager;
using ProjectB.Item;

public class CombinationUIPresenter : MonoBehaviour
{
    // 인벤토리

    [SerializeField] List<Slot> slots = new List<Slot>();
    [SerializeField] List<Slot> combinationSlots = new List<Slot>();
    [SerializeField] List<Item> combinationItems = new List<Item>();
    [SerializeField] List<Item> combinationResourcesItems = new List<Item>();
    int[] requirematerials;
    public string lastPress;

    // 조합소
    public void SwapOnCombination(Slot slot)
    {
        if (lastPress == "InventorySlot")
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slot.IsClicked && slots[i].IsClicked)
                {
                    if (slot == slots[i])
                    {
                        continue;
                    }

                    if (slots[i].GetComponent<Item>().ItemType == ItemType.Exapandable)
                    {
                        if (slots[i].GetComponent<Item>().Code % 100 > 10)
                        {
                            slot.GetComponent<Item>().SwapItem(slots[i].GetComponent<Item>());
                            slot.GetComponent<Item>().Text_Test.text = slot.GetComponent<Item>().ItemName;
                            slots[i].GetComponent<Item>().Text_Test.text = slots[i].GetComponent<Item>().ItemName;
                            DisplayToCombinationResourcesSlot();
                            slot.IsClicked = false;
                            slots[i].IsClicked = false;
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
        }
        if (lastPress == "CombinationSlot")
        {
            for (int i = 0; i < combinationSlots.Count; i++)
            {
                if (slot.IsClicked && combinationSlots[i].IsClicked)
                {
                    slot.gameObject.GetComponent<Item>().SwapItem(combinationSlots[i].gameObject.GetComponent<Item>());
                    slot.GetComponent<Item>().Text_Test.text = slot.GetComponent<Item>().ItemName;
                    combinationSlots[i].GetComponent<Item>().Text_Test.text = combinationSlots[i].GetComponent<Item>().ItemName;
                    DisplayToCombinationResourcesSlot();
                    slot.IsClicked = false;
                    combinationSlots[i].IsClicked = false;
                }
                break;
            }
        }
    }

    public void OnClickCombinationItemButton()
    {
        bool isCombination = false;
        for (int i = 0; i < 4; i++)
        {
            if (GameDataManager.Instance.PlayerGamedata[3000 + i] < requirematerials[i])
                isCombination = false;
        }

        if (isCombination)
        {                                                                                                                                                                                                                                                                                                                             
            Debug.Log("조합성공");
        }
        else
        {
            Debug.Log("실패");
        }
    }

    public void DisplayToCombinationResourcesSlot()
    {
        combinationResourcesItems[0].Text_Test.text = combinationItems[0].RecipeBrick.ToString();
        combinationResourcesItems[1].Text_Test.text = combinationItems[0].RecipeWood.ToString();
        combinationResourcesItems[2].Text_Test.text = combinationItems[0].RecipeIron.ToString();
        combinationResourcesItems[3].Text_Test.text = combinationItems[0].RecipeSheep.ToString();

        requirematerials[0] = combinationItems[0].RecipeWood;
        requirematerials[1] = combinationItems[0].RecipeIron;
        requirematerials[2] = combinationItems[0].RecipeBrick;
        requirematerials[3] = combinationItems[0].RecipeSheep;
    }
}
