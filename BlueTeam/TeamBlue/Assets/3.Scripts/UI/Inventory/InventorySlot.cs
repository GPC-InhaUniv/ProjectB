using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public bool isEmpty;

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
        isEmpty = true;
        UpdateIamge();
    }

    public void UpdateIamge()
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
        Debug.Log("인벤토리 클릭");
       inventory.SwapOnclick(this);     
    }
}
