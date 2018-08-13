using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.Inventory
{
    class Recipe
    {
        public string recipeName;
        public int code;
        public int wood;
        public int iron;
        public int sheep;
        public int brick;

        public Recipe()
        {
            this.code = 0;
            this.wood = 0;
            this.iron = 0;
            this.sheep = 0;
            this.brick = 0;
        }

        public Recipe(int code, int wood, int iron, int sheep, int brick)
        {
            this.code = code;
            this.wood = wood;
            this.iron = iron;
            this.sheep = sheep;
            this.brick = brick;
        }
    }
    class Item
    {
        public bool stackable;
        public string itemName;
        public int code;
        public int attack;
        public int defence;
        public int hp;

        public Item()
        {
            this.stackable = true;
            this.code = 0;
            this.attack = 0;
            this.defence = 0;
            this.hp = 0;
        }

        public Item(int code, int attack, int defence, int hp, bool stackable)
        {
            this.stackable = stackable;
            this.code = code;
            this.attack = attack;
            this.defence = defence;
            this.hp = hp;
        }
    }
    public class Inventory_Test : MonoBehaviour
    {
        [SerializeField] GameObject inventoryPanel;
        [SerializeField] GameObject slotPanel;
        [SerializeField] GameObject inventorySlotPanel;
        [SerializeField] GameObject inventoryItemImage;
        [SerializeField] Sprite[] sprites;

        const int slotAmount = 20;
        List<Item> items = new List<Item>();
        List<GameObject> slots = new List<GameObject>();


        private void Awake()
        {
            for (int i = 0; i < slotAmount; i++)
            {
                items.Add(new Item());
                slots.Add(Instantiate(inventorySlotPanel));
                slots[i].transform.SetParent(slotPanel.transform);
            }
            AddItem(0);
            AddItem(0);
            AddItem(0);
            AddItem(0);
            AddItem(0);
            AddItem(0);
            AddItem(0);
            AddItem(0);
            AddItem(1);
        }

        public void AddItem(int id)
        {
            Item ItemToAdd = new Item();

            if(ItemToAdd.stackable && CheckIfItemIsInInventory(ItemToAdd))
            {
                for(int i = 0; i < items.Count; i++)
                {
                    if(items[i].code == id)
                    {
                        ItemData itemData = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                        itemData.amount++;
                        itemData.transform.GetChild(0).GetComponent<Text>().text = itemData.amount.ToString();
                        break;
                    }
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                if(slots[i].transform.childCount < 1)
                {
                    items[i] = ItemToAdd;
                    GameObject ItemObj = Instantiate(inventoryItemImage);
                    ItemObj.GetComponent<Image>().sprite = sprites[id];
                    ItemObj.transform.SetParent(slots[i].transform);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        bool CheckIfItemIsInInventory(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].code == item.code)
                    return true;
            }

            return false;
        }
    }
}