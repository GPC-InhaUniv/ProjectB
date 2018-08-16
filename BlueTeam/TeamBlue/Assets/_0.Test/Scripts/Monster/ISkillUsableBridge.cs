using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    enum Bossskill
    {
        SkillFireRain,
        SkillHemiSphere,
        SkillFireEntangle,
    }
    public interface ISkillUsableBridge
    {
        void UseSkill();

    }

    public class NoSkill : ISkillUsableBridge
    {
        Monster monster;
        public Monster Monster
        {
            get { return monster; }
            private set { monster = value; }
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
    public class NamedSkill : ISkillUsableBridge
    {
        Monster monster;
        public Monster Monster
        {
            get { return monster; }
            private set { monster = value; }
        }
        GameObject skillPrefab;

        public NamedSkill(Monster monster, GameObject[] skillPrefab)
        {
            Monster = monster;
            this.skillPrefab = skillPrefab[0];
        }

        public void UseSkill()
        {
            Debug.Log(Monster.transform.position);
            //(공격,소환)스킬 오브젝트 풀에서 받아와서 사용할 예정//

            skillPrefab.transform.position = Monster.transform.position;
            skillPrefab.SetActive(true);

            Monster.animator.SetBool(AniStateParm.SkillOne.ToString(),true);
        }

    }
    public class BossSkillFirst : ISkillUsableBridge
    {
        Boss boss;
        public Boss Boss
        {
            get { return boss; }
            private set { boss = value; }
        }
        public BossSkillFirst(Boss boss, GameObject[] skillPrefab)
        {

            Boss = boss;
        }
        public void UseSkill()
        {
            Boss.ChangeState(Monster.State.Chasing);

            Debug.Log("boss state useskill");
        }
    }
    public class BossSkillSecond : MonoBehaviour, ISkillUsableBridge
    {
        Boss boss;
        public Boss Boss
        {
            get { return boss; }
            private set { boss = value; }
        }
        GameObject SkillTest;
        public BossSkillSecond(Boss boss, GameObject[] skillPrefab)
        {
            Boss = boss;
            SkillTest = skillPrefab[(int)Bossskill.SkillFireRain];
        }
        public void UseSkill()
        {
            Boss.animator.SetBool(AniStateParm.SkillOne.ToString(), true);

            Instantiate(SkillTest,Boss.transform);

            Debug.Log("boss state useskill");
        }
    }
    public class BossSkillDefence : ISkillUsableBridge
    {
        Boss boss;
        public Boss Boss
        {
            get { return boss; }
            private set { boss = value; }
        }
        GameObject SkillTest;
        public BossSkillDefence(Boss boss, GameObject[] skillPrefab)
        {
            Boss = boss;
            SkillTest = skillPrefab[(int)Bossskill.SkillHemiSphere];
        }
        public void UseSkill()
        {
            //Instantiate(SkillTest);

            Debug.Log("boss state useskill");
        }
    }
    public class BossSkillThird : MonoBehaviour, ISkillUsableBridge
    {
        Boss boss;
        public Boss Boss
        {
            get { return boss; }
            private set { boss = value; }
        }
        GameObject SkillTest;
        public BossSkillThird(Boss boss, GameObject[] skillPrefab)
        {
            Boss = boss;
            SkillTest = skillPrefab[(int)Bossskill.SkillFireEntangle];
        }
        public void UseSkill()
        {
            Instantiate(SkillTest, Boss.attackTarget);

            Debug.Log("boss state useskill");
        }
    }

}
