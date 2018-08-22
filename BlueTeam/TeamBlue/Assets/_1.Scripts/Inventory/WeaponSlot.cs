using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace ProjectB.Inventory
{
    using Item;

    public class WeaponSlot : Slot
    {
        [SerializeField] EquipUIPresenter equipUIPresenter;

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!isClicked)
                isClicked = true;
            else
                isClicked = false;

            if (beforePressSlot != null)
            {
                if (this.isClicked && beforePressSlot.IsClicked)
                    if (beforePressSlot.gameObject.tag == SlotType.InventorySlot.ToString())
                        equipUIPresenter.SwapToFromEquipWeaponSlotToInventorySlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());

                this.InitializeToIsClicked();
                beforePressSlot.InitializeToIsClicked();
                beforePressSlot.InitializeTobeforePressSlot();
            }
            else
                beforePressSlot = eventData.pointerEnter.gameObject.GetComponent<Slot>();
        }
    }
}