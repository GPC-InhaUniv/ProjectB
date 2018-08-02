using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mediator : Singleton<Test_Mediator>
{
    protected Test_Mediator() { }

    IDamageInteractionable DamageReceiver;
    IPositionInteractionable PositionReceiver;
    IQuestInteractionable QuestReceiver;
  
    public void SendTarget(IDamageInteractionable target, int damage)
    {
        DamageReceiver = target;
        DamageReceiver.ReceiveDamage(damage);

    }

//미니맵 Script 및 미니맵의 Tag 필요 
    public void SendPosition(Vector3 playerPosition)
    {
        //       if (PositionReceiver == null)
        //           PositionReceiver = GameObject.FindGameObjectWithTag("MiniMap").GetComponent<Minimap>();

        PositionReceiver.ReceivePosition(playerPosition);
    }

    public void SendQuest()
    {

    }



}
