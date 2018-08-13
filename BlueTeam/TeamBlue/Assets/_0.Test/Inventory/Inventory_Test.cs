using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectB.Inventory
{
    class Recipe
    {
        string recipeName;
        int code;
        int wood;
        int iron;
        int sheep;
        int brick;

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
        string itemName;
        int code;
        int attack;
        int defence;
        int hp;

        public Item()
        {
            this.code = 0;
            this.attack = 0;
            this.defence = 0;
            this.hp = 0;
        }

        public Item(int code, int attack, int defence, int hp)
        {
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
<<<<<<< HEAD
        [SerializeField] GameObject inventorySlotPanel;
        [SerializeField] GameObject inventoryItemImage;

        const int slotAmount = 20;
        List<Item> Items = new List<Item>();
        List<GameObject> slots = new List<GameObject>();

        private void Start()
        {
            for(int i = 0; i < slotAmount; i++)
            {
                slots.Add(Instantiate(inventorySlotPanel));
                slots[i].transform.SetParent(slotPanel.transform);
            }
=======
        [SerializeField] GameObject itemImage;
        [SerializeField] GameObject inventorySlot;
        [SerializeField] Sprite[] sprites;

        const int slotAmount = 20;
        List<Item> items = new List<Item>();
        List<GameObject> slots = new List<GameObject>();

        private void Awake()
        {
            for (int i = 0; i < slotAmount; i++)
            {
                items.Add(new Item());
                slots.Add(Instantiate(inventorySlot));
                slots[i].transform.SetParent(slotPanel.transform);
            }
            AddItem(0);
            AddItem(1);
        }

        public void AddItem(int id)
        {
            Item ItemToAdd = new Item();
            for (int i = 0; i < items.Count; i++)
            {
                if(slots[i].transform.childCount < 1)
                {
                    items[i] = ItemToAdd;
                    GameObject ItemObj = Instantiate(itemImage);
                    ItemObj.GetComponent<Image>().sprite = sprites[id];
                    ItemObj.transform.SetParent(slots[i].transform);
                    break;
                }
                else
                {
                    continue;
                }
            }
>>>>>>> 9485cb3a0ecafac901b24162a48539a8e2020441
        }
    }
}