using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public interface IAttackableBridge
    {
        void Attack();
    }
    public class NormalAttack : IAttackableBridge
    {
        Animator animator;
        public NormalAttack(Animator animator)
        {
            this.animator = animator;
        }
        public void Attack()
        {
            animator.SetInteger(AniStateParm.Attack.ToString(), 1);
        }
    }
    public class ComboAttack : IAttackableBridge
    {
        Animator animator;
        public ComboAttack(Animator animator)
        {
            this.animator = animator;
        }
        public void Attack()
        {
            animator.SetInteger(AniStateParm.Attack.ToString(), 2);
        }
    }
}

