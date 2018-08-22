﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectB.Inventory
{
    using Item;

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
                    if (beforePressSlot.gameObject.tag == SlotType.WarehouseSlot.ToString())
                        warehouseUIPresenter.SwapToInventoryItem(this, beforePressSlot);

                    else if (beforePressSlot.gameObject.tag == SlotType.InventorySlot.ToString())
                        warehouseUIPresenter.SwapToFromCombinationSlotToInventorySlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());
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