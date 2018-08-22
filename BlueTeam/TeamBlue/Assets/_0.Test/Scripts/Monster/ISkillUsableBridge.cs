using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Monsters
{
    public delegate void NoticeNoSkill();
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
        Animator animator;
       // public static event NoticeNoSkill Setstate;
        public static NoticeNoSkill SetState;



        public NoSkill(Animator animator)
        {
            this.animator = animator;
        }

        public void UseSkill()
        {
            SetState();

            //Monster.ChangeState(Monster.State.Chasing);

        }

    }
    public class NamedSkill : ISkillUsableBridge
    {

        Animator animator;
        GameObject skillPrefab;

        public NamedSkill(Animator animator, GameObject[] skillPrefab)
        {
            this.animator = animator;
            this.skillPrefab = skillPrefab[0];

        }


        public void UseSkill()
        {
            //(공격,소환)스킬 오브젝트 풀에서 받아와서 사용할 예정//

            animator.SetBool(AniStateParm.SkillOne.ToString(),true);
            Debug.Log("gogogo");
        }

    }
    public class BossSkillFirst : ISkillUsableBridge
    {
        Animator animator;
        public static NoticeNoSkill SetState;
        public BossSkillFirst(Animator animator, GameObject[] skillPrefab)
        {
            this.animator = animator;
        }

        public void UseSkill()
        {
            SetState();
            //Boss.ChangeState(Monster.State.Chasing);
            Debug.Log("boss state useskill");
        }
    }
    public class BossSkillSecond : MonoBehaviour, ISkillUsableBridge
    {
        Animator animator;
        GameObject SkillTest;

        public BossSkillSecond(Animator animator, GameObject[] skillPrefab)
        {
            this.animator = animator;
            SkillTest = skillPrefab[(int)Bossskill.SkillFireRain];

        }

        public void UseSkill()
        {
            animator.SetBool(AniStateParm.SkillOne.ToString(), true);
            Debug.Log(animator.GetBool(AniStateParm.SkillOne.ToString()));

            Instantiate(SkillTest);

            Debug.Log("boss state useskill");
        }
    }
    public class BossSkillDefence : MonoBehaviour, ISkillUsableBridge
    {
        Animator animator;
        GameObject SkillTest;

        public BossSkillDefence(Animator animator, GameObject[] skillPrefab)
        {
            this.animator = animator;
            SkillTest = skillPrefab[(int)Bossskill.SkillHemiSphere];

        }
        public void UseSkill()
        {
            animator.SetBool(AniStateParm.Defence.ToString(), true);

            Instantiate(SkillTest);

            Debug.Log("boss state useskill");
        }
    }
    public class BossSkillThird : MonoBehaviour, ISkillUsableBridge
    {

        Animator animator;
        GameObject SkillTest;

        public BossSkillThird(Animator animator, GameObject[] skillPrefab)
        {
            this.animator = animator;
            SkillTest = skillPrefab[(int)Bossskill.SkillFireEntangle];

        }
        public void UseSkill()
        {
            //Instantiate(SkillTest, Boss.attackTarget);

            Debug.Log("boss state useskill");
        }
    }

}
