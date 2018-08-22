using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace ProjectB.Inventory
{
    using Item;

    public class InventorySlot : Slot
    {
        [SerializeField] InventoryUIPresenter inventoryUIPresenter;

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!isClicked)
                isClicked = true;
            else
                isClicked = false;

            if (beforePressSlot != null)
            {
                if (this.isClicked && beforePressSlot.IsClicked)
                {
                    if (beforePressSlot.gameObject.tag == SlotType.InventorySlot.ToString())
                        inventoryUIPresenter.SwapToInventoryItem(this, beforePressSlot);

                    else if (beforePressSlot.gameObject.tag == SlotType.CombinationSlot.ToString() || beforePressSlot.gameObject.tag == SlotType.WarehouseSlot.ToString())
                        inventoryUIPresenter.SwapToFromCombinationSlotToInventorySlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());

                    else if (beforePressSlot.gameObject.tag == SlotType.EquipSlot.ToString())
                        inventoryUIPresenter.SwapToFromEquipSlotToInventorySlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());
                }

                this.InitializeToIsClicked();
                beforePressSlot.InitializeToIsClicked();
                beforePressSlot.InitializeTobeforePressSlot();
            }
            else
                beforePressSlot = eventData.pointerEnter.gameObject.GetComponent<Slot>();
        }
    }
}