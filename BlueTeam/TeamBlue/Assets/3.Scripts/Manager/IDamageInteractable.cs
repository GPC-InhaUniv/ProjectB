using UnityEngine;

public interface IDamageInteractable  {

    
    void SendDamage(IDamageInteractable target);
    void ReceiveDamage(int damage);
}

