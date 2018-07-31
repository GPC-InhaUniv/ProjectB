using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Test_ItemSlot : MonoBehaviour , IPointerClickHandler ,IDragHandler,IEndDragHandler
{

    public ISlotable item;
    [SerializeField]
    Image slotImage;

    Test_ItemCarrier ItemCarrier;

    public bool isEmpty;
    Stack<ISlotable> itemSlot;

    int count;

    private void Start()
    {
        ItemCarrier = GameObject.FindGameObjectWithTag("DragImage").GetComponent<Test_ItemCarrier>();       
        slotImage = GetComponent<Image>();
        itemSlot = new Stack<ISlotable>();
        isEmpty = true;
    }

    public void AddItem(ISlotable item)
    {
        itemSlot.Push(item);
        Test_SwordItem test = item as Test_SwordItem;
        UpdateSlot(true, test.sprite);
        isEmpty = false;
    }

    public void UseItem()
    {
        if (isEmpty)
            return;
        itemSlot.Pop();
    }

    void UpdateSlot(bool isSlot, Sprite sprite)
    {
        if (isSlot)
            slotImage.sprite = sprite;
        else
            slotImage.sprite = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // itemCarrier.item = item;

      
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 종료");
    }
}
