using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public class ColliderActivator : MonoBehaviour
    {
        Collider[] attackAreaColliders;
        AttackArea[] attackArea;

        void Start()
        {
            attackArea = GetComponentsInChildren<AttackArea>();
            attackAreaColliders = new Collider[attackArea.Length];

            for (int attackAreaCount = 0; attackAreaCount < attackArea.Length; attackAreaCount++)
            {
                attackAreaColliders[attackAreaCount] = attackArea[attackAreaCount].GetComponent<Collider>();
                attackAreaColliders[attackAreaCount].enabled = false;
            }
        }

        public void AttackStart()
        {
            foreach (Collider attackAreaCollider in attackAreaColliders)
                attackAreaCollider.enabled = true;
        }

        public void AttackEnd()
        {
            foreach (Collider attackAreaCollider in attackAreaColliders)
                attackAreaCollider.enabled = false;
        }
    }
}