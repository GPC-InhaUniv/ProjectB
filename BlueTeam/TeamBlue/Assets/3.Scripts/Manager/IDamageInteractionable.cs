using UnityEngine;

interface IDamageInteractionable  {

    void SendDamage(GameObject target, int damage);
    void ReceiveDamage(int damage);
}
