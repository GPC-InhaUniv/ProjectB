using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Inventory View part가 될 예정
/// </summary>

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{

    public Stack<Item> SlotinItem;
    [SerializeField]
    Image slotImage;
    [SerializeField]
    Text itemCountText;
    Sprite defaltSprite;
    public Sprite itemSprite;
    Inventory inventory;

    int itemCode = 0;
    public Item ItemInfo { get { return SlotinItem.Peek(); }     }
    public int ItemCode { get { return itemCode; }   }
    public bool isNotEmpty;

    Image itemCarrier;

    public bool isClicked;

    // Use this for initialization
    void Start()
    {
        SlotinItem = new Stack<Item>();
        itemCountText = GetComponentInChildren<Text>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

    }

    public void AddItem(Item item)
    {
        SlotinItem.Push(item);
        isNotEmpty = true;
        itemCode = SlotinItem.Peek().Code;
        UpdateImage();
    }

    public Item UseItem()
    {
        Item usedItem;
        if (SlotinItem.Count != 0)
        {
            usedItem = SlotinItem.Pop();
        }
        else
            usedItem = null;

        if(SlotinItem.Count <=0)
        {
            itemCode = 0;
            isNotEmpty = false;
        }

        UpdateImage();
        return usedItem;

    }

    void UpdateImage()
    {
        if (SlotinItem.Count <= 0)
        {
            slotImage.sprite = defaltSprite;
        }
        else
        {
            slotImage.sprite = itemSprite;//SlotinItem.Peek().item.itemSprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
     //   UseItem();
        inventory.SwapOnClick(this);
    }
}
