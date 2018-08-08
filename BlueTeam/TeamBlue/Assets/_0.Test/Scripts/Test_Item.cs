using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test_Item : MonoBehaviour {

    public Inventory inventory;
    public Item item;
    public void BuyItem()
    {
        inventory.AddItem(item);
    }

}
