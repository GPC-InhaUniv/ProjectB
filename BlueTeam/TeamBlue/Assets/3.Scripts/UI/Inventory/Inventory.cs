using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inventory Model이 될 예정
/// </summary>


    public enum InventoryType
    {
        Equipment,
        Resorces,

    }


    public class Inventory : MonoBehaviour
    {


        public List<InventorySlot> SlotList;

        private InventoryType inventoryType = InventoryType.Equipment;
        public InventoryType InventoryType { get { return inventoryType; } }
        void Start()
        {


        }

        void LoadItem(int ItemCode, int count)
        {

        }
        public void AddItem(Item item)
        {
            foreach (InventorySlot slot in SlotList)
            {
                if (!slot.isNotEmpty)
                    continue;
                if (slot.SlotinItem.Peek().Code == item.Code)
                {
                    slot.AddItem(item);
                    return;
                }
            }
            foreach (InventorySlot slot in SlotList)
            {
                if (slot.isNotEmpty)
                    continue;

                slot.AddItem(item);
                return;

            }
        }

        public Item GetItem()
        {
            for (int i = 0; i < SlotList.Count; i++)
            {
                if (SlotList[i].isClicked)
                    return SlotList[i].UseItem();
            }
            return null;
        }

        public void SwapOnClick(InventorySlot NewClickedSlot)
        {
            for (int i = 0; i < SlotList.Count; i++)
            {
                if (SlotList[i].isClicked)
                {
                    SlotList[i].isClicked = false;
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
