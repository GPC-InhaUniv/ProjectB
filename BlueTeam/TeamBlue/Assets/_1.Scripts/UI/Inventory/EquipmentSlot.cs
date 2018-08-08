using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IEquipSlotable, IPointerClickHandler
{


    Inventory inventory;
    [SerializeField]
    EquipmentInventory equipInventory;
    public Item equipItem;

    Image slotImage;
    Sprite defaltSprite;
    public Sprite itemSprite;

    bool isCliecked;
    // Use this for initialization
    void Start()
    {
        slotImage = GetComponent<Image>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        equipInventory = GameObject.FindGameObjectWithTag("EquipInventory").GetComponent<EquipmentInventory>();
    }

    int index = -1;
    public void CheckItemType()
    {
        if (inventory.InventoryType != InventoryType.Equipment)
            return;
        for (int i = 0; i < inventory.SlotList.Count; i++)
        {
            if (!inventory.SlotList[i].isClicked)
            {
                isCliecked = false;
                continue;
            }
            else
            {
                if (inventory.SlotList[i].SlotinItem.Peek().Code / 1000 == 2)
                {
                    index = i;
                    isCliecked = true;
                }
            }
        }     
    }

    public void EquipItemToSlot()
    {   
        equipItem = CheckEquipList(inventory.GetItem());
        UpdateIamge();
    }


    Item CheckEquipList(Item item)
    {
        foreach (EquipmentSlot slot in equipInventory.slotList)
        {
            if (slot.equipItem == null)     
                continue;  
            else
            {
                if (slot.equipItem.Code % 10 == item.Code % 10)
                {
                    inventory.AddItem(slot.equipItem);
                    slot.equipItem = null;
                    slot.UpdateIamge();
                }
            }
        }
        
        return item;
    }
    public void UpdateIamge()
    {
        if (equipItem != null)
            slotImage.sprite = itemSprite;//SlotinItem.Peek().item.itemSprite;
        else
            slotImage.sprite = defaltSprite;
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        EquipItemToSlot();

    }
}
