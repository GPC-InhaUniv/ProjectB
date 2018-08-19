#define InventorySlot
#define CombinationSlot

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    bool isClicked;
    public bool IsClicked
    {
        get { return isClicked; }
        set { isClicked = value; }
    }
    [SerializeField] InventoryUIPresenter InventoryUIPresenter;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isClicked) 
        {
            isClicked = true;
        }
        else
        {
            isClicked = false;
        }

    //    if (eventData.pointerEnter.gameObject.tag == "InventorySlot" && InventoryUIPresenter.lastPress == "InventorySlot")
    //        InventoryUIPresenter.SwapOnInventoryItem(this);
    //    else if ((eventData.pointerEnter.gameObject.tag == "CombinationSlot" && InventoryUIPresenter.lastPress == "InventorySlot") || (eventData.pointerEnter.gameObject.tag == "InventorySlot" && villageUIPresenter.lastPress == "CombinationSlot"))
    //        InventoryUIPresenter.SwapOnCombination(this);

    //    villageUIPresenter.lastPress = eventData.pointerEnter.gameObject.tag;
    }
}
