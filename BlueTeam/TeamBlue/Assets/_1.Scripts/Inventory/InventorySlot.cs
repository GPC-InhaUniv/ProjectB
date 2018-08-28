using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace ProjectB.Inventory
{
    using Item;

    public class InventorySlot : Slot
    {
        [SerializeField] InventoryUIPresenter inventoryUIPresenter;

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!isClicked)
            {
                isClicked = true;
                clickedImage.color = new Color(1, 1, 1, 1);
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
                    if (beforePressSlot.gameObject.tag == SlotType.InventorySlot.ToString())
                        inventoryUIPresenter.SwapToInventoryItem(this, beforePressSlot);

                    else if (beforePressSlot.gameObject.tag == SlotType.CombinationSlot.ToString() || beforePressSlot.gameObject.tag == SlotType.WarehouseSlot.ToString())
                        inventoryUIPresenter.SwapToFromCombinationSlotToInventorySlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());

                    else if (beforePressSlot.gameObject.tag == SlotType.EquipSlot.ToString())
                        inventoryUIPresenter.SwapToFromEquipSlotToInventorySlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());
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