//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using ProjectB.Item;

//namespace ProjectB.Inventory
//{
//    public class Inventory_Test : MonoBehaviour
//    {
//        [SerializeField] List<Item> items = new List<Item>();
//        [SerializeField] List<Slot> slots = new List<Slot>();

//        public void AddItem(int code)
//        {
//            for (int i = 0; i < items.Count; i++)
//            {
//                if(items[i].Code == code)
//                {
//                    if(items[i].ItemType != ItemType.Equipmentable)
//                    {
//                        Debug.Log("갯수 증가");
//                    }
//                    else
//                    {
//                        continue;
//                    }
//                    break;
//                }

//                else if(items[i].Code == 0)
//                {
//                    items[i].SetItem(code);
//                    items[i].Text_Test.text = items[i].ItemName;
                    
//                    break;
//                }
//            }
//        }

//        public void SwapOnClick(Slot slot)
//        {
//            int SlotIndex;

//            for(int i = 0; i < slots.Count; i++)
//            {
//                if (slot.IsClicked && slots[i].IsClicked)
//                {
//                    if(slot == slots[i])
//                    {
//                        continue;
//                    }
//                    SlotIndex = slot.transform.GetSiblingIndex();
//                    slot.transform.SetSiblingIndex(slots[i].transform.GetSiblingIndex());
//                    slots[i].transform.SetSiblingIndex(SlotIndex);
//                    slot.IsClicked = false;
//                    slots[i].IsClicked = false;
//                    break;
//                }
//                else
//                {
//                    continue;
//                }
//            }
//        }
//    }
//}