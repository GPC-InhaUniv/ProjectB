using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.Item;
using UnityEngine.EventSystems;

public class HelmetSlot : Slot
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
            {
                if (beforePressSlot.gameObject.tag == "InventorySlot")
                {
                    equipUIPresenter.SwapToFromEquipHelmetSlotToInventorySlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());
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
