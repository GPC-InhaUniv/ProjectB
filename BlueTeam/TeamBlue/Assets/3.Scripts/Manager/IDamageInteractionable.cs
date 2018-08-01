using UnityEngine;

public interface IDamageInteractionable  {

    
    void SendDamage(IDamageInteractionable target);
    void ReceiveDamage(int damage);
}

