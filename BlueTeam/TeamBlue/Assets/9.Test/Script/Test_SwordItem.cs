using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test_SwordItem : MonoBehaviour, ISlotable
{
    [SerializeField]
    public Sprite sprite;

    Image image;
    [SerializeField]
    Test_Inventory inventory;
    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprite;
    }

    public void TestAddItem()
    {
        inventory.AddItem(this);    
    }
}
