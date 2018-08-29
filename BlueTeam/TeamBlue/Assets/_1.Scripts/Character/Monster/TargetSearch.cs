using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public class TargetSearch : MonoBehaviour
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
                monster.SetAttackTarget(other.transform);
        }

    }
}