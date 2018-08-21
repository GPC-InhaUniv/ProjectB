using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Item;
using UnityEngine.EventSystems;

public class WarehouseSlot : Slot
{
    [SerializeField] WarehouseUIPresenter warehouseUIPresenter;

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

                if (beforePressSlot.gameObject.tag == "WarehouseSlot")
                {
                    warehouseUIPresenter.SwapToInventoryItem(this, beforePressSlot);
                }
                else if (beforePressSlot.gameObject.tag == "InventorySlot")
                {
                    warehouseUIPresenter.SwapToFromCombinationSlotToInventorySlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());
                }
            }
            this.InitializeToIsClicked();
            beforePressSlot.InitializeToIsClicked();
            beforePressSlot.InitializeTobeforePressSlot();
        }
        else
        {
            beforePressSlot = eventData.pointerEnter.gameObject.GetComponent<Slot>();
        }
    }
}
