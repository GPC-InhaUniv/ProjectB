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
                StopCoroutine(AttackCoroutine(attackName));
            }
            attackingCoroutine = StartCoroutine(AttackCoroutine(attackName));
        }

        public void SkillAnimation(string skillName)
        {
            if (skillCoroutine != null)
            {
                StopCoroutine(SkillCoroutine(skillName));
            }
            skillCoroutine = StartCoroutine(SkillCoroutine(skillName));
        }

        public void BackStepAnimation()
        {
            if (backStepCoroutine != null)
            {
                StopCoroutine(BackStepCoroutine());
            }
            backStepCoroutine = StartCoroutine(BackStepCoroutine());
        }

        public void RunAnimation(bool isRunning)
        {
            animator.SetBool(AnimationState.Run.ToString(), isRunning);
        }

        IEnumerator AttackCoroutine(string attackName)
        {
            animator.SetBool(attackName, true);
            yield return new WaitForSeconds(1.0f);
            animator.SetBool(attackName, false);
        }

        IEnumerator SkillCoroutine(string skillName)
        {
            animator.SetBool(skillName, true);
            yield return new WaitForSeconds(2.0f);
            animator.SetBool(skillName, false);
        }

        IEnumerator BackStepCoroutine()
        {
            animator.SetBool(AnimationState.BackStep.ToString(), true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool(AnimationState.BackStep.ToString(), false);
        }

        public void HitAnimation()
        {
            animator.SetTrigger(AnimationState.Hit.ToString());
        }

        public void Weapon(PlayerCharacterWeaponState weaponState)
        {
            StartCoroutine(PreSwapCoroutine());
            animator.SetBool(AnimationState.LongSword.ToString(), false);
            animator.SetBool(AnimationState.ShortSword.ToString(), false);

            animator.SetBool(weaponState.ToString(), true);
        }


        IEnumerator PreSwapCoroutine()
        {
            animator.SetBool(AnimationState.Swap.ToString(), true);
            yield return new WaitForSeconds(0.2f);
            animator.SetBool(AnimationState.Swap.ToString(), false);
        }


        public void InitWeapon()
        {
            animator.SetBool(AnimationState.ShortSword.ToString(), true);
        }

        public void DieAnimation()
        {
            animator.SetBool(AnimationState.Die.ToString(), true);
            animator.SetTrigger(AnimationState.Die.ToString());
        }

    }

}

