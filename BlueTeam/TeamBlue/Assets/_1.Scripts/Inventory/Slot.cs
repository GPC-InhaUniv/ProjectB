using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ProjectB.Inventory;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    bool isClicked;
    public bool IsClicked
    {
        get { return isClicked; }
        set { isClicked = value; }
    }
    [SerializeField] VillageUIPresenter villageUIPresenter;

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

        if(eventData.pointerEnter.gameObject.tag == "InventorySlot" && villageUIPresenter.lastPress == "InventorySlot")
            villageUIPresenter.SwapOnClick(this);
        else if((eventData.pointerEnter.gameObject.tag == "CombinationSlot" && villageUIPresenter.lastPress == "InventorySlot") || (eventData.pointerEnter.gameObject.tag == "InventorySlot" && villageUIPresenter.lastPress == "CombinationSlot"))
            villageUIPresenter.SwapOnCombination(this);

        villageUIPresenter.lastPress = eventData.pointerEnter.gameObject.tag;
    }
}
