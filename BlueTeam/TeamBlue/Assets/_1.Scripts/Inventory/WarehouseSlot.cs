using System.Collections;
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
            {
                isClicked = true;
                ClickedImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                isClicked = false;
                this.InitializeToClickedImage();
            }

            if (beforePressSlot != null)
            {
                if (this.isClicked && beforePressSlot.IsClicked)
                {
                    if (beforePressSlot.gameObject.tag == SlotType.WarehouseSlot.ToString())
                        warehouseUIPresenter.SwapToInventoryItem(this, beforePressSlot);

                    else if (beforePressSlot.gameObject.tag == SlotType.InventorySlot.ToString())
                        warehouseUIPresenter.SwapToFromInventorySlotToWarehouseSlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());
                }

                this.InitializeToIsClicked();
                this.InitializeToClickedImage();
                beforePressSlot.InitializeToClickedImage();
                beforePressSlot.InitializeToIsClicked();
                beforePressSlot.InitializeTobeforePressSlot();
            }
            else
                beforePressSlot = eventData.pointerEnter.gameObject.GetComponent<Slot>();
        }
    }
}