using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    Animator animator;

    Coroutine attackingCoroutine, backStepCoroutine;

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

    public void BackStepAnimation()
    {
        if (attackingCoroutine != null)
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
        yield return new WaitForSeconds(1.0f);
        animator.SetBool(attackName, false);
    }


    IEnumerator BackStepCoroutine()
    {
        animator.SetBool("Back", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Back", false);
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


}
