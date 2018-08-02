using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{

    public Stack<Item> SlotinItem;
    Image slotImage;
    [SerializeField]
    Text itemCountText;
    Sprite defaltSprite;

    Inventory inventory;

    public bool isEmpty;

    Image itemCarrier;

    public bool isClicked;

    // Use this for initialization
    void Start()
    {
        itemCountText = GetComponentInChildren<Text>();
        itemCarrier = GameObject.FindGameObjectWithTag("DragImage").GetComponent<Image>();

    }

    public void AddItem(Item item)
    {
        SlotinItem.Push(item);
        isEmpty = true;
    }

    public void UpdateIamge()
    {
        if (SlotinItem.Count <= 0)
        {
            slotImage.sprite = defaltSprite;
        }
        else
        {
            // slotImage.sprite = SlotinItem.Peek().item.itemSprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       inventory.SwapOnclick(this);     
    }
}
