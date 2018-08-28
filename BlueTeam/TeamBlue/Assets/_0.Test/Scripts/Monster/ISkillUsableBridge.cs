using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectB.GameManager;

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
        public static NoticeNoSkill SetState;

        
        public NoSkill(Animator animator)
        {
            this.animator = animator;
        }

        public void UseSkill()
        {
            SetState();
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
        }
    }
    public class BossSkillSecond : ISkillUsableBridge
    {
        Animator animator;
        GameObject SkillTest;
        Transform target;

        public BossSkillSecond(Animator animator,Transform target)
        {
            this.animator = animator;
            //SkillTest = skillPrefab[(int)Bossskill.SkillFireRain];
            SkillTest = GameObjectsManager.Instance.BossSkill(KindOfSkill.FireRain);
            this.target = target;

        }

        public void UseSkill()
        {
            animator.SetBool(AniStateParm.SkillOne.ToString(), true);
            SkillTest.gameObject.transform.position = target.position;
        }
    }
    public class BossSkillDefence :  ISkillUsableBridge
    {
        Animator animator;
        GameObject SkillTest;
        Transform target;

        public BossSkillDefence(Animator animator, Transform target)
        {
            this.animator = animator;
            //SkillTest = skillPrefab[(int)Bossskill.SkillHemiSphere];
            SkillTest = GameObjectsManager.Instance.BossSkill(KindOfSkill.FireHemiSphere);
            this.target = target;

        }
        public void UseSkill()
        {
            animator.SetBool(AniStateParm.Defence.ToString(), true);
            SkillTest.gameObject.transform.position = target.position;


        }
    }
    public class BossSkillThird : MonoBehaviour, ISkillUsableBridge
    {

        Animator animator;
        GameObject SkillTest;
        Transform target;

        public BossSkillThird(Animator animator, Transform target)
        {
          //  SkillTest = skillPrefab[(int)Bossskill.SkillFireEntangle];
            this.animator = animator;
            SkillTest = GameObjectsManager.Instance.BossSkill(KindOfSkill.FireEntangle);
            this.target = target;

        }
        public void UseSkill()
        {
            animator.SetTrigger(AniStateParm.SkillTwo.ToString());
            SkillTest.gameObject.transform.position = target.position;
        }
    }

}
