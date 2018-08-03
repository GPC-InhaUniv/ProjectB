using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerCode : MonoBehaviour ,IDamageInteractionable {
    public void ReceiveDamage(int damage)
    {
        Debug.Log("데미지 받음, 데미지 : " + damage.ToString());
    }

    public void SendDamage(IDamageInteractionable target)
    {
      
    }

    
}
