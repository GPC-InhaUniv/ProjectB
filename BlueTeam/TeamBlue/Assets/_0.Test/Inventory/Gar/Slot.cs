using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ProjectB.Inventory;

public class Slot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory_Test inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory_Test>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();
    }
}