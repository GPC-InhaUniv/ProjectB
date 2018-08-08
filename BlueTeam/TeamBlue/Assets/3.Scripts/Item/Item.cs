using System;
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

    string itemname;
    public string ItemName { get { return itemname; }}

    ItemType itemType;
    public ItemType ItemType { get { return itemType; } }

    int hp;
    public int Hp { get { return hp; } }
     
    int attack;
    public int Attack { get { return attack; } }

    int defence;
    public int Defence { get { return defence; } }

    int recipeWood;
    public int RecipeWood { get { return recipeWood; } }

    int recipeIron;
    public int RecipeIron { get { return recipeIron; } }

    int recipeSheep;
    public int RecipeSheep { get { return recipeSheep; } }

    int recipeBrick;
    public int RecipeBrick { get { return recipeBrick; } }

    string image;
    public string Image { get { return image; } }

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
                name = itemtable.sheets[0].list[i].Name;
                switch( itemtable.sheets[0].list[i].Type)
                {
                    case "Resouece":
                        itemType = ItemType.Resouece;
                        break;
                    case "Expandable":
                        itemType = ItemType.Exapandable; 
                        break;
                    case "Equipment":
                        itemType = ItemType.Equipmentable;
                        break;
                }

                hp = itemtable.sheets[0].list[i].HP;
                attack = itemtable.sheets[0].list[i].Attack;
                recipeWood = itemtable.sheets[0].list[i].RecipeWood;
                recipeSheep = itemtable.sheets[0].list[i].RecipeSheep;
                recipeIron = itemtable.sheets[0].list[i].RecipeIron;
                recipeBrick = itemtable.sheets[0].list[i].RecipeBrick;
                image= itemtable.sheets[0].list[i].Image;



            }
        }
        if (GameDataManager.Instance.PlayerGamedata.ContainsKey(code))
            GameDataManager.Instance.PlayerGamedata[code] = 1;
        else
            GameDataManager.Instance.PlayerGamedata.Add(Code, 1);
    }
}
