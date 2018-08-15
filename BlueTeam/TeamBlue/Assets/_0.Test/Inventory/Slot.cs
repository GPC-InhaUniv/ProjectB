using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ProjectB.Inventory;

/*IPointerDownHandler*/
//IPointerEnterHandler

public class Slot : MonoBehaviour, IPointerClickHandler
{
    bool isClicked;
    public bool IsClicked
    {
        get { return isClicked; }
        set { isClicked = false; }
    }
    [SerializeField] Inventory_Test inventory_Test;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isClicked) 
        {
            isClicked = true;
        }
        else
        {
            isClicked = false;
        }
        inventory_Test.SwapOnClick(this);
    }
}
