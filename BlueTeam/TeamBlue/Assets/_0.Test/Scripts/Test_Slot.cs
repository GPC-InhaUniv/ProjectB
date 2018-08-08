using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
public class Test_Slot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    
    public Stack<Test_Item> slot;
    public Image slotImage;
    public Text itemCountText;
    Sprite defaltSprite;

    Test_Inventory inventory;
    public bool isEmpty;

    Image itemCarrier;

    private void Start()
    {
        itemCountText = GetComponentInChildren<Text>();
        itemCarrier = GameObject.FindGameObjectWithTag("DragImage").GetComponent<Image>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Test_Inventory>();
        slot = new Stack<Test_Item>();
        slotImage = GetComponent<Image>();

    }

    public void AddItem(Test_Item item)
    {
        slot.Push(item);
        UpdateInfo();
        isEmpty = true;
    }

    public void UpdateInfo()
    {
        if (slot.Count <= 0)
            slotImage.sprite = defaltSprite;

        else
            slotImage.sprite = slot.Peek().ItemSprite;

        itemCountText.text = slot.Count.ToString();

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (slot.Count <= 0)
            return;
        itemCarrier.gameObject.SetActive(true);
        itemCarrier.sprite = slotImage.sprite;
        itemCarrier.gameObject.transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slot.Count <= 0)
            return;
        slotImage.sprite = itemCarrier.sprite;
        inventory.SwapSlot(this, itemCarrier.gameObject.transform.position);
        itemCarrier.gameObject.SetActive(false);

        // UpdateInfo();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
     
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
*/
