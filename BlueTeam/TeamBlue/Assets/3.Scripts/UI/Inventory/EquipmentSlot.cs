using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour , IEquipSlotable , IPointerClickHandler {


    Inventory inventory;
    [SerializeField]
    EquipmentInventory equipInventory;
    public Item equipItem;

    Image slotImage;
    Sprite defaltSprite;

    bool isCliecked;
    // Use this for initialization
    void Start () {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();	
	}

    int index = -1;
    public void CheckItemType()
    {

        for(int i = 0; i< inventory.slotList.Count; i++)
        {
            if (!inventory.slotList[i].isClicked)
            {
                isCliecked = false;
                continue;
            }
            else
            {
                if (inventory.slotList[i].SlotinItem.Peek().Code / 1000 == 2)
                {
                    index = i;
                    isCliecked = true;
                }
            }
        }
        /*
        foreach(InventorySlot item in inventory.slotList)
        {
            if(!item.isClicked)
                continue;
            else
            {
                if (item.SlotinItem.Peek().Code % 1000 == 2)
                {
                    equipItem = item.SlotinItem.Peek();
             //       EquipItemToSlot();
                }
                else
                    return;
            }
        }*/
    }

    public void EquipItemToSlot()
    {
        CheckEquipList(inventory.slotList[index].SlotinItem.Pop());
        inventory.slotList[index].isEmpty = false;
    }


    bool CheckEquipList(Item item)
    {
        if (item == null)
            return false;
        foreach(EquipmentSlot slot in equipInventory.slotList)
        {
            if (slot.equipItem == null)
                continue;
            else
            {
                if (slot.equipItem.Code % 10 == equipItem.Code % 10)
                {
                    return true;
                }
                else
                {
                    equipItem = item;
                    return false;
                }
            }
        }
        return false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCliecked)
            isCliecked = true;
        else if(index != -1)
        {
            EquipItemToSlot();
        }
    }
}
