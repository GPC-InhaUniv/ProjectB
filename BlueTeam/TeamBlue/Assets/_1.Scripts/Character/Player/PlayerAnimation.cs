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

        Coroutine attackingCoroutine, backStepCoroutine, skillCoroutine;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void AttackAnimation(string attackName)
        {
            if (attackingCoroutine != null)
            {
                StopCoroutine(AttackAnimationCoroutine(attackName));
            }
            attackingCoroutine = StartCoroutine(AttackAnimationCoroutine(attackName));
        }

        public void SkillAnimation(string skillName)
        {
            if (skillCoroutine != null)
            {
                StopCoroutine(SkillAnimationCoroutine(skillName));
            }
            skillCoroutine = StartCoroutine(SkillAnimationCoroutine(skillName));
        }

        public void BackStepAnimation()
        {
            if (backStepCoroutine != null)
            {
                StopCoroutine(BackStepAnimationCoroutine());
            }
            backStepCoroutine = StartCoroutine(BackStepAnimationCoroutine());
        }

        public void RunAnimation(bool isRunning)
        {
            animator.SetBool(AnimationState.Run.ToString(), isRunning);
        }

        public void HitAnimation()
        {
            animator.SetTrigger(AnimationState.Hit.ToString());
        }

        public void WeaponSwapAnimation(PlayerCharacterWeaponState weaponState)
        {
            StartCoroutine(SwapAnimationCoroutine());
            animator.SetBool(AnimationState.LongSword.ToString(), false);
            animator.SetBool(AnimationState.ShortSword.ToString(), false);

            animator.SetBool(weaponState.ToString(), true);
        }
 
        public void InitWeapon()
        {
            animator.SetBool(AnimationState.ShortSword.ToString(), true);
        }

        public void DieAnimation()
        {
            animator.SetTrigger(AnimationState.Die.ToString());
        }

        public void InitStateAnimation()
        {
            StartCoroutine(InitStateCoroutine());
        }

        IEnumerator InitStateCoroutine()
        {
            animator.SetBool(AnimationState.Init.ToString(), true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool(AnimationState.Init.ToString(), false);
        }
        IEnumerator SwapAnimationCoroutine()
        {
            animator.SetBool(AnimationState.Swap.ToString(), true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool(AnimationState.Swap.ToString(), false);
        }
        IEnumerator AttackAnimationCoroutine(string attackName)
        {
            animator.SetBool(attackName, true);
            yield return new WaitForSeconds(0.7f);
            animator.SetBool(attackName, false);
        }
        IEnumerator SkillAnimationCoroutine(string skillName)
        {
            animator.SetBool(skillName, true);
            yield return new WaitForSeconds(2.0f);
            animator.SetBool(skillName, false);
        }
        IEnumerator BackStepAnimationCoroutine()
        {
            animator.SetBool(AnimationState.BackStep.ToString(), true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool(AnimationState.BackStep.ToString(), false);
        }
    }

}

