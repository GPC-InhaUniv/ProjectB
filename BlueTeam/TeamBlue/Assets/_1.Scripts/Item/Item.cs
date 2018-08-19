using System;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.GameManager;

namespace ProjectB.Item
{
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
        Text text_Test;
        public Text Text_Test { get { return text_Test; } }

        [SerializeField]
        Image image_Test;
        public Image Image_Test { get { return image_Test; } }

        public int Code;

        string itemname;
        public string ItemName { get { return itemname; } }

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

        public void SetItem(int code)
        {
            Code = code;

            if (code == 0)
            {
                initializationItem();
                return;
            }
            for (int i = 0; i < itemtable.sheets[0].list.Count; i++)
            {
                if (itemtable.sheets[0].list[i].Code == Code)
                {
                    itemname = itemtable.sheets[0].list[i].Name;
                    switch (itemtable.sheets[0].list[i].Type)
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
                    image = itemtable.sheets[0].list[i].Image;
                }
            }
            //if (GameDataManager.Instance.PlayerGamedata.ContainsKey(code))
            //    GameDataManager.Instance.PlayerGamedata[code] = 1;
            //else
            //    GameDataManager.Instance.PlayerGamedata.Add(Code, 1);
        }

        public void SwapItem(Item item)
        {
            int i = this.Code;
            int j = item.Code;

            this.SetItem(j);
            item.SetItem(i);
        }

        public void initializationItem()
        {
            hp = 0;
            attack = 0;
            recipeWood = 0;
            recipeSheep = 0;
            recipeIron = 0;
            recipeBrick = 0;
            image = "";
            itemname = "";
        }
    }
}