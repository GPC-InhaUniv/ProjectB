using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Character.Monster
{
    public class ColiderActivator : MonoBehaviour
    {
        Collider[] attackAreaColliders;
        AttackArea[] attackArea;

        // Use this for initialization
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
            {
                attackAreaCollider.enabled = true;
                Debug.Log("공격 시작");
            }
        }

        public void AttackEnd()
        {

            foreach (Collider attackAreaCollider in attackAreaColliders)
            {
                attackAreaCollider.enabled = false;
                Debug.Log("공격 끝");
            }
        }
    }
}