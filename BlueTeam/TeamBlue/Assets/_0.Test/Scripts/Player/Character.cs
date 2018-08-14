using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectB.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]
        protected int characterHealthPoint;
        public int CharacterHealthPoint { get { return characterHealthPoint; } private set { } }

        protected int characterMaxHealthPoint;
        public int CharacterMaxHealthPoint { get { return characterMaxHealthPoint; } private set { } }

        [SerializeField]
        protected int characterLevel { get { return characterExp / 100; } set { } }
        public int CharacterLevel { get { return characterLevel; } private set { } }

        [SerializeField]
        protected int characterExp;

        [SerializeField]
        protected int characterDefensivePower;

        protected int characterAttackPower;
        public int CharacterAttackPower { get { return characterAttackPower; } private set { } }

        public abstract void ReceiveDamage(int damage);
    }
}