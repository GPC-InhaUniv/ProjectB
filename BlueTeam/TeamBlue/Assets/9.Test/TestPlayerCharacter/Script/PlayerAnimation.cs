using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

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
        animator.SetBool("Run", isRunning);
    }



    IEnumerator AttackCoroutine(string attackName)
    {
        animator.SetBool(attackName, true);
        yield return new WaitForSeconds(0.4f);
        animator.SetBool(attackName, false);
    }

    IEnumerator SkillCoroutine(string skillName)
    {
        animator.SetBool(skillName, true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool(skillName, false);
    }

    IEnumerator BackStepCoroutine()
    {
        animator.SetBool("Back", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Back", false);
    }

    public void HitAnimation()
    {
        animator.SetTrigger("Hot");
    }

    public void WeaponSwap(PlayerCharacterWeaponState weaponState)
    {
        StartCoroutine(PreSwapCoroutine());
        animator.SetBool("LongSword", false);
        animator.SetBool("ShortSword", false);

        animator.SetBool(weaponState.ToString(), true);
    }


    IEnumerator PreSwapCoroutine()
    {
        animator.SetBool("Swap", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Swap", false);
    }


    public void InitWeapon()
    {
        animator.SetBool("ShortSword", true);
    }

    public void DieAnimation()
    {
        animator.SetBool("Die", true);
    }

    //죽은 메소드

}
