using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Character.Monster
{
    public interface ISkillUsable
    {
        void UseSkill();
    }

    public class NoSkill : ISkillUsable
    {
        Monster monster;
        public Monster Monster
        {
            get { return monster; }
            set { monster = value; }
        }
        public NoSkill(Monster monster)
        {
            Monster = monster;
        }

        public void UseSkill()
        {
            Monster.ChangeState(Monster.State.Chasing);
        }

    }
    public class NamedSkill : ISkillUsable
    {
        Monster monster;
        public Monster Monster
        {
            get { return monster; }
            set { monster = value; }
        }
        GameObject skillPrefab;

        public NamedSkill(Monster monster, GameObject skillPrefab)
        {
            Monster = monster;
            this.skillPrefab = skillPrefab;
        }

        public void UseSkill()
        {
            Debug.Log(Monster.transform.position);
            //(공격,소환)스킬 오브젝트 풀에서 받아와서 사용할 예정//

            skillPrefab.transform.position = Monster.transform.position;
            skillPrefab.SetActive(true);
            //Monster.animator.SetInteger("Attack", 3);

            Monster.animator.SetBool(AniStateParm.Skill.ToString(),true);
            ///anim.SetInteger("Attack", 3);


        }

    }
    public class BossSkillFirst : ISkillUsable
    {
        Boss boss;
        public Boss Boss
        {
            get { return boss; }
            set { boss = value; }
        }
        public BossSkillFirst(Boss boss, GameObject skillPrefab)
        {

            Boss = boss;
        }
        public void UseSkill()
        {
            Boss.ChangeState(Monster.State.Chasing);

            Debug.Log("boss state useskill");
        }
    }
    public class BossSkillSecond : MonoBehaviour, ISkillUsable
    {
        Boss boss;
        public Boss Boss
        {
            get { return boss; }
            set { boss = value; }
        }
        GameObject SkillTest;
        public BossSkillSecond(Boss boss, GameObject skillPrefab)
        {
            Boss = boss;
            SkillTest = skillPrefab;
        }
        public void UseSkill()
        {
            Instantiate(SkillTest);

            Debug.Log("boss state useskill");
        }
    }
    public class BossSkillThird : ISkillUsable
    {
        //public Monster Monster
        //{
        //    get { return Monster; }
        //    set { Monster = value; }
        //}
        public void UseSkill()
        {


        }
    }
}
