using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public class BossTargetSearch : MonoBehaviour
    {
        [SerializeField]
        Monster monster;
        void Start()
        {
            monster = GetComponent<Monster>();
        }

        void OnTriggerStay(Collider other)
        {

            // Player태그를 타깃으로 한다.
            if (other.CompareTag("Player"))
            {
                float fieldOfView = 120.0f;
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                Debug.Log(angle);

                if (angle < fieldOfView * 0.5)
                {
                    monster.SetAttackTarget(other.transform);
                }
            }
        }
    }
}