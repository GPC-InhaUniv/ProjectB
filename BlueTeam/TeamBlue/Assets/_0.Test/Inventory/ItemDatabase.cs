using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public Item Item;

    private void Start()
    {
        Item.SetItem(3000);
        Debug.Log(Item.ItemName);
    }
}
