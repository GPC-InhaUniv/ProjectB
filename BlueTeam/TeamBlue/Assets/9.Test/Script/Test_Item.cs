using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test_Item : MonoBehaviour {

	public enum ItemType
    {
        EquipItem,
        ConsumItem,
        ResourceItem,
        
    }

    public ItemType itemtype;
    [SerializeField]
    private Sprite itemSprite;
    public Sprite ItemSprite { get { return itemSprite; }}

    public int MaxCount;

    Image test;

    Test_Inventory inventory;


    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Test_Inventory>();
        test = GetComponent<Image>();
        test.sprite = itemSprite;
    }

    public void BuyItem()
    {
        inventory.AddItem(this);
    }

}
