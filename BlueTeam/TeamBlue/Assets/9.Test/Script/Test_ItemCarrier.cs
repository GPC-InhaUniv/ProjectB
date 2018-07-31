using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Test_ItemCarrier : MonoBehaviour {
    [SerializeField]
    public ISlotable item;
    public Stack<ISlotable> itemSlot;
    public Image image;


    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void SlotUpdate()
    {

    }

}
