using System.Collections;
using UnityEngine;
namespace ProjectB.Characters.Players
{
    public enum AnimationState
    {
        LongSword,
        ShortSword,
        Run,
        Die,
        Init,
        Hit,
        Swap,
        BackStep,
        Attack,
        Skill
    }

    public class PlayerAnimation : MonoBehaviour
    {

        [HideInInspector]
        public Animator animator;

        bool isRunningAttackCoroutine, isRunningBackStepCoroutine, isRunningSkillCoroutine;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void InitStateAnimation()
        {
            StartCoroutine(InitStateCoroutine());
        }

        public void AttackAnimation(string attackName)
        {
            if (isRunningAttackCoroutine == false)
                StartCoroutine(AttackAnimationCoroutine(attackName));
        }

        public void SkillAnimation()
        {
            if (isRunningSkillCoroutine == false)
                StartCoroutine(SkillAnimationCoroutine());
        }

        public void BackStepAnimation()
        {
            if (isRunningBackStepCoroutine == false)
             StartCoroutine(BackStepAnimationCoroutine());
        }


        public void RunAnimation(bool isRunning)
        {
            animator.SetBool(AnimationState.Run.ToString(), isRunning);
        }

        public void HitAnimation()
        {
            animator.SetTrigger(AnimationState.Hit.ToString());
        }

        public void ResetHitTrigger()
        {
            animator.ResetTrigger(AnimationState.Hit.ToString());
        }

        public void WeaponSwapAnimation(PlayerCharacterWeaponState weaponState)
        {
            StartCoroutine(SwapAnimationCoroutine());

            animator.SetBool(AnimationState.LongSword.ToString(), false);
            animator.SetBool(AnimationState.ShortSword.ToString(), false);

            animator.SetBool(weaponState.ToString(), true);
        }
 


        public void DieAnimation()
        {
            animator.SetTrigger(AnimationState.Die.ToString());
        }

        IEnumerator InitStateCoroutine()
        {
            animator.SetBool(AnimationState.Init.ToString(), true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool(AnimationState.Init.ToString(), false);
        }
        IEnumerator SwapAnimationCoroutine()
        {
            ResetHitTrigger();
            animator.SetBool(AnimationState.Swap.ToString(), true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool(AnimationState.Swap.ToString(), false);
        }
        IEnumerator AttackAnimationCoroutine(string attackName)
        {
            ResetHitTrigger();
            isRunningAttackCoroutine = true;
            animator.SetBool(attackName, true);
            yield return new WaitForSeconds(1.0f);
            animator.SetBool(attackName, false);
            isRunningAttackCoroutine = false;
        }
        IEnumerator SkillAnimationCoroutine()
        {
            ResetHitTrigger();
            isRunningSkillCoroutine = true;
            animator.SetBool(AnimationState.Skill.ToString(), true);
            yield return new WaitForSeconds(2.0f);
            animator.SetBool(AnimationState.Skill.ToString(), false);
            isRunningSkillCoroutine = false; 
        }
        IEnumerator BackStepAnimationCoroutine()
        {
            ResetHitTrigger();
            isRunningBackStepCoroutine = true;
            animator.SetBool(AnimationState.BackStep.ToString(), true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool(AnimationState.BackStep.ToString(), false);
            isRunningBackStepCoroutine = false;
        }
    }

}

