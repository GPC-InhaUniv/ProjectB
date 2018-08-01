using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Mediator : Singleton<Test_Mediator>
{
    protected Test_Mediator() { }

    Object Sender;
    IModelColleague Recever;

    //IDamageInteractionable DamageSender;
   IDamageInteractionable DamageReceiver;


  //  public IDamageInteractionable target;
    public void SendTarget(IDamageInteractionable target, int damage)
    {
        DamageReceiver = target;
        DamageReceiver.ReceiveDamage(damage);

    }

    public void SendPosition()
    {

    }

    public void SendQuest()
    {

    }
    
   
	
}
