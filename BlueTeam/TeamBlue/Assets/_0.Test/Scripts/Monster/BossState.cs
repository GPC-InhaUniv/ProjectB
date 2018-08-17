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
            this.attackable = attackable;
            defencSkillUsable = new BossSkillDefence(this.animator, SkillPrefab);
            this.defencSkillUsable = defencSkillUsable;
            skillUsable = new BossSkillFirst(this.animator, SkillPrefab);
            this.skillUsable = skillUsable;

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
            this.attackable = attackable;
            defencSkillUsable = new BossSkillDefence(this.animator, SkillPrefab);
            this.defencSkillUsable = defencSkillUsable;
            skillUsable = new BossSkillSecond(this.animator, SkillPrefab);
            this.skillUsable = skillUsable;


            Debug.Log(skillUsable);

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
          //  skillUsable.UseSkill();
            defencSkillUsable.UseSkill();

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
            this.attackable = attackable;

            defencSkillUsable = new BossSkillDefence(this.animator, SkillPrefab);
            this.defencSkillUsable = defencSkillUsable;

            skillUsable = new BossSkillSecond(this.animator, SkillPrefab);
            this.skillUsable = skillUsable;

            entangleSkillUsable = new BossSkillThird(this.animator, SkillPrefab);
            this.entangleSkillUsable = entangleSkillUsable;

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

