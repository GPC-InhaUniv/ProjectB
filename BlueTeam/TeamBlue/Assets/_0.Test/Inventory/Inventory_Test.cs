using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Inventory
{
    public class Inventory_Test : MonoBehaviour
    {
        const int slotAmount = 35;
        [SerializeField] List<Item> items = new List<Item>();
        [SerializeField] GameObject slotPanel;
        [SerializeField] GameObject inventorySlot;

        private void Awake()
        {
            for(int i = 0; i < slotAmount; i++)
            {
                GameObject invenTemp = Instantiate(inventorySlot);
                invenTemp.transform.SetParent(slotPanel.transform);
                items.Add(invenTemp.GetComponent<Item>());
            }
        }


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
    }
}
