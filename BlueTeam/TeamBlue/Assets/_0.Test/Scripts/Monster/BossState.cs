using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    [System.Serializable]
    public abstract class BossState
    {

        Boss boss;
        public Boss Boss
        {
            get { return boss; }
            set { boss = value; }
        }
        protected GameObject[] SkillPrefab;

        public abstract void Attack();
        public abstract void UseSkill();
        public abstract void UseDefenceSkill();



    }
    public class PhaseOne : BossState
    {
        public PhaseOne(Boss boss, GameObject[] skillPrefab)
        {
            Boss = boss;
            SkillPrefab = skillPrefab;

            Boss.Attackable = new NormalAttack(Boss);
            Boss.SkillUsable = new BossSkillFirst(Boss, SkillPrefab);
            Boss.DefencSkillUsable = new BossSkillDefence(Boss, SkillPrefab);




        }
        public override void Attack()
        {
            Boss.Attackable.Attack();
        }

        public override void UseDefenceSkill()
        {
            Boss.DefencSkillUsable.UseSkill();
        }

        public override void UseSkill()
        {
            Boss.SkillUsable.UseSkill();
        }
    }
    public class PhaseTwo : BossState
    {
        public PhaseTwo(Boss boss, GameObject[] skillPrefab)
        {
            Boss = boss;
            SkillPrefab = skillPrefab;

            Boss.Attackable = new ComboAttack(Boss);
            Boss.SkillUsable = new BossSkillSecond(Boss, SkillPrefab);
            Boss.DefencSkillUsable = new BossSkillDefence(Boss, SkillPrefab);

        }
        public override void Attack()
        {
            Boss.Attackable.Attack();
        }

        public override void UseDefenceSkill()
        {
            Boss.DefencSkillUsable.UseSkill();
        }

        public override void UseSkill()
        {
            Boss.SkillUsable.UseSkill();
        }
    }
}

