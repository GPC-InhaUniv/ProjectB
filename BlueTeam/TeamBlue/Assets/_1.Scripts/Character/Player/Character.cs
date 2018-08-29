using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectB.Characters
{
    public abstract class Character : MonoBehaviour
    {
        protected float healthPoint;
        public float HealthPoint { get { return healthPoint; } private set { } }

        protected float maxHealthPoint;
        public float MaxHealthPoint { get { return maxHealthPoint; } private set { } }

        protected int level;
        public int Level { get { return level; } private set { } }

        protected float exp;
        public float Exp { get { return exp; } private set { } }

        protected float attackPower;
        public float AttackPower { get { return attackPower; } private set { } }

        protected float defensivePower;

        public abstract void ReceiveDamage(float damage);
    }
}