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

        protected IAttackableBridge attackable;
        protected ISkillUsableBridge skillUsable;
        protected ISkillUsableBridge defencSkillUsable;
        protected ISkillUsableBridge entangleSkillUsable;

        public abstract void Attack();
        public abstract void UseSkill();
        public abstract void UseDefenceSkill();

    }
    public class PhaseOne : BossState
    {
        public PhaseOne(Animator animator, GameObject[] skillPrefab, 
            IAttackableBridge attackable, ISkillUsableBridge defencSkillUsable,ISkillUsableBridge skillUsable)
        {
            this.animator = animator;
            SkillPrefab = skillPrefab;

            attackable = new NormalAttack(this.animator);
            defencSkillUsable = new BossSkillDefence(this.animator, SkillPrefab);
            skillUsable = new BossSkillFirst(this.animator, SkillPrefab);

            attackable.Attack();


        }
        public override void Attack()
        {
            attackable.Attack();
        }

        public override void UseDefenceSkill()
        {
            defencSkillUsable.UseSkill();
        }

        public override void UseSkill()
        {
            skillUsable.UseSkill();
        }
    }
    public class PhaseTwo : BossState
    {
        public PhaseTwo(Animator animator, GameObject[] skillPrefab,
            IAttackableBridge attackable, ISkillUsableBridge defencSkillUsable, ISkillUsableBridge skillUsable)
        {
            this.animator = animator;
            SkillPrefab = skillPrefab;

            attackable = new NormalAttack(this.animator);
            defencSkillUsable = new BossSkillDefence(this.animator, SkillPrefab);
            skillUsable = new BossSkillFirst(this.animator, SkillPrefab);

            attackable.Attack();


        }
        public override void Attack()
        {
            attackable.Attack();
        }

        public override void UseDefenceSkill()
        {
            defencSkillUsable.UseSkill();
        }

        public override void UseSkill()
        {
            skillUsable.UseSkill();
        }
    }
    public class PhaseThree : BossState
    {
        public PhaseThree(Animator animator, GameObject[] skillPrefab, IAttackableBridge attackable,
            ISkillUsableBridge defencSkillUsable,ISkillUsableBridge skillUsable,ISkillUsableBridge entangleSkillUsable)
        {
            this.animator = animator;
            SkillPrefab = skillPrefab;

            attackable = new NormalAttack(this.animator);
            defencSkillUsable = new BossSkillDefence(this.animator, SkillPrefab);
            skillUsable = new BossSkillFirst(this.animator, SkillPrefab);
            entangleSkillUsable = new BossSkillThird(this.animator, SkillPrefab);
            attackable.Attack();


        }
        public override void Attack()
        {
            attackable.Attack();
        }

        public override void UseDefenceSkill()
        {
            defencSkillUsable.UseSkill();
        }

        public override void UseSkill()
        {
            int probability = Random.Range(1, 4);
            if (probability == 1)
            {
                skillUsable.UseSkill();
            }
            else
            {
                entangleSkillUsable.UseSkill();
            }
        }
    }
}

