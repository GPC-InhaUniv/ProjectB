using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

abstract public class Slot : MonoBehaviour, IPointerClickHandler
{
    protected bool isClicked;
    public bool IsClicked { get { return isClicked; } }
    protected string lastPressSlot;
    protected static Slot beforePressSlot;

    abstract public void OnPointerClick(PointerEventData eventData);

    public void InitializeToIsClicked()
    {
        isClicked = false;
    }

    public void InitializeTobeforePressSlot()
    {
        beforePressSlot = null;
    }
}
