using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectB.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        protected float healthPoint;
        public float HealthPoint { get { return healthPoint; } private set { } }

        [SerializeField]
        protected float maxHealthPoint;
        public float MaxHealthPoint { get { return maxHealthPoint; } private set { } }

        [SerializeField]
        protected int level;
        public int Level { get { return level; } private set { } }

        [SerializeField]
        protected float exp;
        public float Exp { get { return exp; } private set { } }

        [SerializeField]
        protected float attackPower;
        public float AttackPower { get { return attackPower; } private set { } }

        [SerializeField]
        protected float defensivePower;



        public abstract void ReceiveDamage(float damage);
    }
}