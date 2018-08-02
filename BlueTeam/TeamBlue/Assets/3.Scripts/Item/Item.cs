using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Resouece,
    Exapandable,
    Equipmentable,

}

public class Item : MonoBehaviour
{
    [SerializeField]
    ItemTable itemtable;

    [SerializeField]
    InputField testInputField;
    int itemIndex;

    public int Code;
    string Name;
    ItemType ItemType;
    int Hp;
    int Attack;
    int Defence;
    int RecipeWood;
    int RecipeIron;
    int RecipeSheep;
    int RecipeBrick;
    string Image;

    private void Start()
    {
     
    }

    public void TestMakeItem()
    {
        Code = Convert.ToInt32(testInputField.text);
    }


    public void SetItem(int code)
    {
        Code = code;
        for (int i = 0; i < itemtable.sheets[0].list.Count; i++)
        {
            if (itemtable.sheets[0].list[i].Code == Code)
            {
                Name = itemtable.sheets[0].list[i].Name;
                switch( itemtable.sheets[0].list[i].Type)
                {
                    case "Resouece":
                        ItemType = ItemType.Resouece;
                        break;
                    case "Expandable":
                        ItemType = ItemType.Exapandable; 
                        break;
                    case "Equipment":
                        ItemType = ItemType.Equipmentable;
                        break;
                }

                Hp = itemtable.sheets[0].list[i].HP;
                Attack = itemtable.sheets[0].list[i].Attack;
                RecipeWood = itemtable.sheets[0].list[i].RecipeWood;
                RecipeSheep = itemtable.sheets[0].list[i].RecipeSheep;
                RecipeIron = itemtable.sheets[0].list[i].RecipeIron;
                RecipeBrick = itemtable.sheets[0].list[i].RecipeBrick;
                Image= itemtable.sheets[0].list[i].Image;



            }
        }

    }
}
