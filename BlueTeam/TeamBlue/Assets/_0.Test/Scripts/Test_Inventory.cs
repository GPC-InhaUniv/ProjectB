using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Inventory : MonoBehaviour
{
    /*
    [SerializeField]
    List<Test_Slot> slotList;

    Test_Slot temp;

    private void Start()
    {
        temp = GameObject.FindGameObjectWithTag("DragImage").GetComponent<Test_Slot>();
    }

    public void AddItem(Test_Item item)
    {
        foreach (Test_Slot slot in slotList)
        {
            if (!slot.isEmpty)
                continue;
            if (slot.slot.Peek().itemtype == item.itemtype)
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach (Test_Slot slot in slotList)
        {
            if (slot.isEmpty)
                continue;

            slot.AddItem(item);
            return;
        }
    }

    Test_Slot NearDisSlot(Vector3 pos)
    {
        float min = 10000f;
        int index = -1;

        for (int i = 0; i < slotList.Count; i++)
        {
            Vector2 comparePos = slotList[i].transform.position;
            float distance = Vector2.Distance(comparePos, pos);
            if (distance < min)
            {
                min = distance;
                index = i;
            }

        }

        return slotList[index];
    }

    public void SwapSlot(Test_Slot slot, Vector3 carrierPos)
    {

        Test_Slot nearSlot = NearDisSlot(carrierPos);

        if (slot == nearSlot)
            return;

        if (!nearSlot.isEmpty)
        {
            

            temp.slot = slot.slot;
            temp.isEmpty = slot.isEmpty;

            slot.slot = nearSlot.slot;
            slot.isEmpty = nearSlot.isEmpty;

            nearSlot.slot = temp.slot;
            nearSlot.isEmpty = temp.isEmpty;

            slot.UpdateInfo();
            nearSlot.UpdateInfo();
        }
    }
    */
    public Test_Inventory()
    {
    }
}
