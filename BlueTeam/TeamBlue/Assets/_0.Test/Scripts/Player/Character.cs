using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectB.Character
{
    public abstract class Character : MonoBehaviour
    {

        public int CharacterHealthPoint;
        protected int CharacterMaxHealthPoint;

        [SerializeField]
        protected int CharacterDefensivePower;


        [SerializeField]
        protected int CharacterLevel;


        public int CharacterAttackPower;

        public int CharacterExp;

        int temp;
        //플레이어는 레벨을 서버에서 받아오며 공식으로 hp 저장


        public abstract void ReceiveDamage(int damage);

        public virtual int SendValue(StatusType statusType)
        {

            switch (statusType)
            {
                case (StatusType.CharacterExp):
                    return temp = CharacterExp;
                case (StatusType.CharacterHealthPoint):
                    return temp = CharacterHealthPoint;

            }
            return temp;

        }

        public abstract void SaveValue(int vlaue);

        public enum StatusType
        {
            CharacterHealthPoint,
            CharacterDefensivePower,
            CharacterLevel,
            CharacterAttackPower,
            CharacterExp
        }
    }
}