using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectB.Inventory
{
    

    abstract public class Slot : MonoBehaviour, IPointerClickHandler
    {
        protected enum SlotType
        {
            InventorySlot, CombinationSlot, EquipSlot, WarehouseSlot
        }
        //protected const string inventorySlot = "InventorySlot";
        //protected const string combinationSlot = "CombinationSlot";
        //protected const string equipSlot = "EquipSlot";
        //protected const string warehouseSlot = "WarehouseSlot";

        protected bool isClicked;
        public bool IsClicked { get { return isClicked; } }
        protected static Slot beforePressSlot;

        abstract public void OnPointerClick(PointerEventData eventData);

        public void InitializeToIsClicked()
        {
            isClicked = false;
        }

        public void InitializeTobeforePressSlot()
        {
            beforePressSlot = null;
        }
    }
}