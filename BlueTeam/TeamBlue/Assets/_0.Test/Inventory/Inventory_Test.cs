using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Inventory
{
    public class Inventory_Test : MonoBehaviour
    {
        const int slotAmount = 35;
        [SerializeField] List<Item> items = new List<Item>();
        [SerializeField] List<Slot> slots = new List<Slot>();
        [SerializeField] GameObject inventorySlot;


        public void AddItem(int code)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].Code == code)
                {
                    if(items[i].ItemType != ItemType.Equipmentable)
                    {
                        Debug.Log("갯수 증가");
                    }
                    else
                    {
                        continue;
                    }
                    break;
                }

                else if(items[i].Code == 0)
                {
                    items[i].SetItem(code);
                    items[i].Text_Test.text = items[i].ItemName;
                    break;
                }
            }
        }

        public void SwapOnClick(Slot slot)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if (slot.IsClicked && slots[i].IsClicked)
                {
                    if(slot == slots[i])
                    {
                        continue;
                    }
                    Transform Parent = slot.gameObject.transform.parent;
                    //Destroy(slot.gameObject.transform.parent);
                    slot.transform.SetParent(slots[i].transform.parent);
                    slots[i].transform.SetParent(Parent);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}