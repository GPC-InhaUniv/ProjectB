using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [SerializeField]
    public List<InventorySlot> slotList;

    Item itemCarrier;

    // Use this for initialization
    void Start () {
	//	itemCarrier = itemCarrier = GameObject.FindGameObjectWithTag("DragImage").GetComponent<Item>();


    }
	
	public void AddItem(Item item)
    {
        foreach(InventorySlot slot in slotList)
        {
            if (!slot.isEmpty)
                continue;
            if(slot.SlotinItem.Peek().Code == item.Code)
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach (InventorySlot slot in slotList)
        {
            if (slot.isEmpty)
                continue;
           
                slot.AddItem(item);
                return;
            
        }
    }

    public void SwapOnclick(InventorySlot NewClickedSlot)
    {
        for(int i = 0; i < slotList.Count;i++)
        {
            if(slotList[i].isClicked)
            {
                slotList[i].isClicked = false;
                NewClickedSlot.isClicked = true;
                return;
            }
            else
            {
                NewClickedSlot.isClicked = true;
            }
        }
    }
}
