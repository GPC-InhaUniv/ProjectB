using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectB.Inventory
{
    using Item;

    public class CombinationSlot : Slot
    {
        [SerializeField] CombinationUIPresenter combinationUIPresenter;
        public CombinationUIPresenter CombinationUIPresenter { get { return combinationUIPresenter; } }

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
                    if (beforePressSlot.gameObject.tag == SlotType.InventorySlot.ToString())
                        combinationUIPresenter.SwapToFromInventorySlotToCombinationSlot(this.gameObject.GetComponent<Item>(), beforePressSlot.gameObject.GetComponent<Item>());

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