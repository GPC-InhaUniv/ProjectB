using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    [System.Serializable]
    public abstract class BossState
    {
        public Transform PlayerTransform;
        public Transform MonsterTransform;

        protected Animator animator;
        protected IAttackableBridge attackable;
        protected ISkillUsableBridge skillUsable;
        protected ISkillUsableBridge defencSkillUsable;
        protected ISkillUsableBridge entangleSkillUsable;

        public abstract void Attack();
        public abstract void UseSkill(Transform transform);
        public abstract void UseDefenceSkill(Transform transform);
    }
    public class PhaseOne : BossState
    {
        public PhaseOne(Animator animator)           
        {
            this.animator = animator;
            attackable = new NormalAttack(this.animator);
            skillUsable = new NoSkill(this.animator);
        }

        public override void Attack()
        {
            attackable.Attack();
        }
        
        public override void UseDefenceSkill(Transform transform)
        {
            MonsterTransform = transform;
            defencSkillUsable = new BossSkillDefence(animator, MonsterTransform);
            defencSkillUsable.UseSkill();
        }

        public override void UseSkill(Transform transform)
        {
            skillUsable.UseSkill();
        }
    }
    public class PhaseTwo : BossState
    {
        public PhaseTwo(Animator animator)            
        {
            this.animator = animator;
            attackable = new ComboAttack(this.animator);
        }

        public override void Attack()
        {
            attackable.Attack();
        }

        public override void UseDefenceSkill(Transform transform)
        {
            MonsterTransform = transform;
            defencSkillUsable = new BossSkillDefence(animator, MonsterTransform);
            defencSkillUsable.UseSkill();
        }

        public override void UseSkill(Transform transform)
        {
            PlayerTransform = transform;
            skillUsable = new BossSkillSecond(animator, PlayerTransform);
            Debug.Log("gogogo");
            skillUsable.UseSkill();
        }
    }

    public class PhaseThree : BossState
    {
        public PhaseThree(Animator animator)    
        {
            this.animator = animator;
            attackable = new NormalAttack(this.animator);
        }
        public override void Attack()
        {
            attackable.Attack();
        }

        public override void UseDefenceSkill(Transform transform)
        {
            MonsterTransform = transform;
            defencSkillUsable = new BossSkillDefence(animator, MonsterTransform);
            defencSkillUsable.UseSkill();
        }

        public override void UseSkill(Transform transform)
        {
            PlayerTransform = transform;
            int probability = Random.Range(1, 4);
            if (probability == 1)
            {
                skillUsable = new BossSkillSecond(animator, PlayerTransform);
                skillUsable.UseSkill();
            }
            else
            {
                entangleSkillUsable = new BossSkillThird(animator, PlayerTransform);
                entangleSkillUsable.UseSkill();
            }
        }
    }
}

