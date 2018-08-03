using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum CombinationItem
{
    Weapon = 0,
    Hat,
    Armor,
    Sheep,
}


public class ItemCombination : MonoBehaviour
{

    [SerializeField]
    Item currentItem;

    int requiredBrickCount;
    int requiredWoodCount;
    int requiredIronCount;
    int requiredSheepCount;
   
    [SerializeField]
    Button[] combinationWeaponButtons;

    [SerializeField]
    Button[] combinationArmorButtons;

    private void Start()
    {
        currentItem = GetComponent<Item>();
        requiredWoodCount = currentItem.RecipeWood;
        requiredIronCount = currentItem.RecipeIron;
        requiredSheepCount = currentItem.RecipeSheep;
        requiredBrickCount = currentItem.RecipeBrick;
    }

    public void OnClickCombinationButton()
    {
          
    }
   




}


