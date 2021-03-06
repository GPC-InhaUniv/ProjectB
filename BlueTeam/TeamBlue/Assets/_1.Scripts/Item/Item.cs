﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ProjectB.Item
{
    public enum ItemType
    {
        Resource,
        Exapandable,
        Equipmentable,
    }

    public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        ItemTable itemtable;

        [SerializeField]
        Text itemAmounttext;
        public Text ItemAmountText { get { return itemAmounttext; } }

        [SerializeField]
        Text itemNameText;
        public Text ItemNameText { get { return itemNameText; } }

        [SerializeField]
        Image itemImage;
        public Image ItemImage { get { return itemImage; } }

        public int Code;

        int itemAmount;
        public int ItemAmount { get { return itemAmount; } }
        
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
                InitializationItem();
                return;
            }

            for (int i = 0; i < itemtable.sheets[0].list.Count; i++)
            {
                if (itemtable.sheets[0].list[i].Code == Code)
                {
                    itemname = itemtable.sheets[0].list[i].Name;
                    switch (itemtable.sheets[0].list[i].Type)
                    {
                        case "Resource":
                            itemType = ItemType.Resource;
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
                    itemAmount = 0;
                    itemAmount++;

                    itemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, this.image);
                }
            }
        }

        public void InitializationItem()
        {
            this.Code = 0;
            this.hp = 0;
            this.attack = 0;
            this.recipeWood = 0;
            this.recipeSheep = 0;
            this.recipeIron = 0;
            this.recipeBrick = 0;
            this.itemAmount = 0;
            this.itemNameText.text = null;
            this.itemImage.sprite = null;
            this.image = null;
            this.itemname = null;
        }

        public void SetItemAmount(int amount)
        {
            this.itemAmount = amount;
        }

        public void IncreaseItemAmount()
        {
            this.itemAmount++;
        }

        public void DecreaseItemAmount()
        {
            this.itemAmount--;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.itemImage.sprite = null;
            this.itemNameText.text = this.ItemName;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.itemNameText.text = null;
            this.itemImage.sprite = AssetBundleManager.Instance.LoadSprite(BundleType.Common, this.image);
        }
    }
}