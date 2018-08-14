using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public interface IAttackable
    {

        Monster Monster { get; set; }

        void Attack();


    }
    public class NormalAttack : IAttackable
    {
        Monster monster;

        public Monster Monster
        {
            get { return monster; }
            set { monster = value; }
        }
        public NormalAttack(Monster monster)
        {
            Monster = monster;
        }
        public void Attack()
        {
            Monster.animator.SetInteger(AniStateParm.Attack.ToString(), 1);
        }

        //private Boss boss;
        //public NormalAttack(Boss boss)
        //{
        //    this.boss = boss;
        //}
    }
    public class ComboAttack : IAttackable
    {
        Monster monster;
        public Monster Monster
        {
            get { return monster; }
            set { monster = value; }
        }
        public ComboAttack(Monster monster)
        {
            Monster = monster;
        }
        public void Attack()
        {
            Monster.animator.SetInteger(AniStateParm.Attack.ToString(), 2);
        }


    }
}

