using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectB.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        protected float characterHealthPoint;
        public float CharacterHealthPoint { get { return characterHealthPoint; } private set { } }

        [SerializeField]
        protected float characterMaxHealthPoint;
        public float CharacterMaxHealthPoint { get { return characterMaxHealthPoint; } private set { } }

        [SerializeField]
        protected int characterLevel;
        public int CharacterLevel { get { return characterLevel; } private set { } }

        [SerializeField]
        protected float characterExp;
        public float CharacterExp { get { return characterExp; } set { } }

        [SerializeField]
        protected float characterDefensivePower;

        [SerializeField]
        protected float characterAttackPower;
        public float CharacterAttackPower { get { return characterAttackPower; }  set { } }

        public abstract void ReceiveDamage(float damage);
    }
}