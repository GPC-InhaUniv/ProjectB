using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.Inventory
{
    class Recipe_Test
    {
        public string recipeName;
        public int code;
        public int wood;
        public int iron;
        public int sheep;
        public int brick;

        public Recipe_Test()
        {
            this.code = 0;
            this.wood = 0;
            this.iron = 0;
            this.sheep = 0;
            this.brick = 0;
        }

        public Recipe_Test(int code, int wood, int iron, int sheep, int brick)
        {
            this.code = code;
            this.wood = wood;
            this.iron = iron;
            this.sheep = sheep;
            this.brick = brick;
        }
    }
    public class Item_Test
    {
        public bool stackable;
        public string itemName;
        public int code;
        public int attack;
        public int defence;
        public int hp;

        public Item_Test()
        {
            this.stackable = true;
            this.code = 0;
            this.attack = 0;
            this.defence = 0;
            this.hp = 0;
        }

        public Item_Test(int code, int attack, int defence, int hp, bool stackable)
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
        List<Item_Test> items = new List<Item_Test>();
        public List<Item_Test> Items = new List<Item_Test>();
        List<GameObject> slots = new List<GameObject>();


        private void Awake()
        {
            for (int i = 0; i < slotAmount; i++)
            {
                items.Add(new Item_Test());
                slots.Add(Instantiate(inventorySlotPanel));
                slots[i].transform.SetParent(slotPanel.transform);
            }
            AddItem(0);
        }

        public void AddItem(int id)
        {
            Item_Test ItemToAdd = new Item_Test();

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

                items[i] = ItemToAdd;
                GameObject ItemObj = Instantiate(inventoryItemImage);
                ItemObj.GetComponent<ItemData>().item = ItemToAdd;
                ItemObj.GetComponent<ItemData>().slot = i;
                ItemObj.GetComponent<Image>().sprite = sprites[id];
                ItemObj.transform.SetParent(slots[i].transform);
                break;
            }
        }

        bool CheckIfItemIsInInventory(Item_Test item)
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