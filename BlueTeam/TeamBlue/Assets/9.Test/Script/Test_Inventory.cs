using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Inventory : MonoBehaviour
{

    public List<Test_ItemSlot> AllSlot;


    public bool AddItem(ISlotable item)
    {

        foreach (Test_ItemSlot slot in AllSlot)
        {

            if (!slot.isEmpty)
                continue;
            if (slot.item == item)
            {

                slot.AddItem(item);
                return true;
            }
        }

        foreach (Test_ItemSlot slot in AllSlot)
        {
            if (!slot.isEmpty)
                continue;
            slot.AddItem(item);
            return true;
        }
        return false;
    }

    Test_ItemSlot NearDisSlot(Vector3 pos)
    {
        float min = 10000f;
        int index = -1;
        for (int i = 0; i < AllSlot.Count; i++)
        {
            Vector2 sPos = AllSlot[i].transform.position;
            float distance = Vector2.Distance(sPos, pos);
            if (distance < min)
            {
                min = distance;
                index = i;
            }
        }

        return AllSlot[index];
    }

    public void SwapSlotItem(Test_ItemSlot slot)
    {
        Test_ItemSlot FirstSlot = NearDisSlot(slot.transform.position);

        if (FirstSlot == slot)
            return;

        if(FirstSlot.isEmpty)
        {
            FirstSlot = slot;
            slot = null;
        }



    }



}
