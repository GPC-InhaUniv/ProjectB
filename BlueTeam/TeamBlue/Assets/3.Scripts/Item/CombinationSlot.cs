using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

enum CombinationItem
{
    Weapon = 0,
    Hat,
    Armor,
    Sheep,
}


public class CombinationSlot : MonoBehaviour, IEquipSlotable,IPointerClickHandler
{
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    Item currentItem;

    int requiredBrickCount;
    int requiredWoodCount;
    int requiredIronCount;
    int requiredSheepCount;

    bool isRecipeEquiped;

    [SerializeField]
    Text text;
    private void Start()
    {
        // inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    
       
    }

    public void CheckItemType()
    {
        
        isRecipeEquiped = true;

        EquipItemToSlot();
    }

    public void EquipItemToSlot()
    {
        requiredWoodCount = currentItem.RecipeWood;
        requiredIronCount = currentItem.RecipeIron;
        requiredSheepCount = currentItem.RecipeSheep;
        requiredBrickCount = currentItem.RecipeBrick;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentItem.SetItem(1311);
        text.text = currentItem.ItemName;
        CheckItemType();


        if (isRecipeEquiped)
        {
            if (GameDataManager.Instance.PlayerGamedata[3000] >= requiredWoodCount)
            {

                if (GameDataManager.Instance.PlayerGamedata[3001] >= requiredIronCount)
                {

                    if (GameDataManager.Instance.PlayerGamedata[3002] >= requiredBrickCount)
                    {
                        if (GameDataManager.Instance.PlayerGamedata[3001] >= requiredSheepCount)
                        {
                            Debug.Log("조합 성공!");
                            GameDataManager.Instance.PlayerGamedata[3000] = GameDataManager.Instance.PlayerGamedata[3000] - requiredWoodCount;

                            GameDataManager.Instance.PlayerGamedata[3001] = GameDataManager.Instance.PlayerGamedata[3001] - requiredIronCount;

                            GameDataManager.Instance.PlayerGamedata[3002] = GameDataManager.Instance.PlayerGamedata[3002] - requiredBrickCount;

                            GameDataManager.Instance.PlayerGamedata[3001] = GameDataManager.Instance.PlayerGamedata[3001] - requiredSheepCount;

                            GameDataManager.Instance.PlayerGamedata[currentItem.Code] -= 1;

                            GameDataManager.Instance.SetGameDataToServer();
                        }
                        else
                        {
                            Debug.Log("양 부족.");
                        }
                    }
                    else
                    {
                        Debug.Log("흙 부족.");
                    }

                }
                else
                {
                    Debug.Log("철광석 부족.");
                }

            }
            else
            {
                Debug.Log("나무 부족.");
            }
        }
        else
        {
            Debug.Log("레시피 장착이 필요합니다.");
        }
    }
}


