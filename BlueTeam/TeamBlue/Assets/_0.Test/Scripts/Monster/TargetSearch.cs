﻿using System.Collections;
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
            // EnemyCtrl을 미리 준비한다.
            monster = GetComponent<Monster>();
        }

        void OnTriggerStay(Collider other)
        {

            // Player태그를 타깃으로 한다.
            if (other.tag == "Player")
                monster.SetAttackTarget(other.transform);
        }

    }
}