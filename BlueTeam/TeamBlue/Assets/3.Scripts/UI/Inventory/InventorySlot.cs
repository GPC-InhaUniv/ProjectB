using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{

    public Stack<Item> SlotinItem;
    Image slotImage;
    [SerializeField]
    Text itemCountText;
    Sprite defaltSprite;

    Inventory inventory;

    public bool isEmpty;

    Image itemCarrier;

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
        if(SlotinItem.Count<=0)
        {
            slotImage.sprite =  defaltSprite;
        }
        else
        {
           // slotImage.sprite = SlotinItem.Peek().ite
        }
    }
}
