using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    [System.Serializable]
    public abstract class BossState
    {
        protected Animator animator;
        protected GameObject[] SkillPrefab;
        public Transform PlayerTransform;
        public Transform MonsterTransform;

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
        public PhaseOne(Animator animator, 
            IAttackableBridge attackable, ISkillUsableBridge defencSkillUsable,ISkillUsableBridge skillUsable)
        {
            this.animator = animator;
            this.attackable = attackable;
            this.defencSkillUsable = defencSkillUsable;
            this.skillUsable = skillUsable;

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
        public PhaseTwo(Animator animator,
            IAttackableBridge attackable, ISkillUsableBridge defencSkillUsable, ISkillUsableBridge skillUsable)
        {
            this.animator = animator;

            attackable = new ComboAttack(this.animator);
            this.attackable = attackable;
            this.defencSkillUsable = defencSkillUsable;
            this.skillUsable = skillUsable;

        }

        public override void Attack()
        {
            attackable.Attack();
        }

        public override void UseDefenceSkill(Transform transform)
        {
            MonsterTransform = transform;
            defencSkillUsable = new BossSkillDefence(this.animator, MonsterTransform);
            defencSkillUsable.UseSkill();
        }

        public override void UseSkill(Transform transform)
        {
            PlayerTransform = transform;
            skillUsable = new BossSkillSecond(this.animator, PlayerTransform);
            skillUsable.UseSkill();
            //defencSkillUsable.UseSkill();

        }

    }
    public class PhaseThree : BossState
    {
        public PhaseThree(Animator animator,  IAttackableBridge attackable,
            ISkillUsableBridge defencSkillUsable,ISkillUsableBridge skillUsable,ISkillUsableBridge entangleSkillUsable)
        {
            this.animator = animator;

            attackable = new NormalAttack(this.animator);
            this.attackable = attackable;
            this.defencSkillUsable = defencSkillUsable;

            this.skillUsable = skillUsable;

            this.entangleSkillUsable = entangleSkillUsable;


        }
        public override void Attack()
        {
            attackable.Attack();
        }


        public override void UseDefenceSkill(Transform transform)
        {
            MonsterTransform = transform;
            defencSkillUsable = new BossSkillDefence(this.animator, MonsterTransform);
            defencSkillUsable.UseSkill();
        }

        public override void UseSkill(Transform transform)
        {
            PlayerTransform = transform;
            int probability = Random.Range(1, 4);
            if (probability == 1)
            {
                skillUsable = new BossSkillSecond(this.animator, PlayerTransform);
                skillUsable.UseSkill();
            }
            else
            {
                entangleSkillUsable = new BossSkillThird(this.animator, PlayerTransform);
                entangleSkillUsable.UseSkill();
            }
        }
    }
}

