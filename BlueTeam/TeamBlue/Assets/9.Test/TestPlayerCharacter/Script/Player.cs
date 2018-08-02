using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    PlayerAnimation playerAinmaton;
    Rigidbody playerRigidbody;

    public Vector3 moveVector;
    public bool isRunning;

    public bool isSwapAble;

    public int attackNum;
    public int skillNum;
    public PlayerCharacterWeaponState CurrentWeaponState;
    public PlayerCharacterState PlayerState;

    [SerializeField]
    Vector3 backTargetVector;

    WeaponSwap weaponSwap;

    ISkillUsable rangeSkill;

    Coroutine swapCoroutine;

    void Start()
    {
        attackNum = 1;
        backTargetVector = new Vector3(0, 0, 1);

        PlayerState = new PlayerCharacterIdleState(this);
        CurrentWeaponState = PlayerCharacterWeaponState.ShortSword;
        isSwapAble = true;

        playerAinmaton = GetComponent<PlayerAnimation>();
        playerRigidbody = GetComponent<Rigidbody>();
        weaponSwap = GetComponent<WeaponSwap>();

        rangeSkill = new MultiAttackRangeSkill(this);

        playerAinmaton.InitWeapon();
    }

    private void FixedUpdate()
    {

        //뒤로 가는 벡터 방향 저장
        if (moveVector != Vector3.zero)
        {
            backTargetVector = moveVector;
        }

        playerAinmaton.RunAnimation(isRunning);
        PlayerState.Tick(moveVector);
    }

    public void SetState(PlayerCharacterState state)
    {
        if (PlayerState == state)
        {
            return;
        }
        else
        {
            PlayerState = state;
        }

    }

    public void PlayerAttack()
    {
        isSwapAble = false;
        playerAinmaton.AttackAnimation("Attack" + attackNum.ToString());
    }

    public void Running(Vector3 moveVector)
    {
        if(PlayerState.GetType() == typeof(PlayerCharacterAttackState))
        {
            return;
        }
        else
        {
            playerRigidbody.velocity = moveVector * 450 * Time.deltaTime;
        }
    }

    public void Turn(Vector3 moveVector)
    {
        transform.rotation = Quaternion.LookRotation(moveVector);
    }
    
    public void BackStep()
    {
        if (PlayerState.GetType() == typeof(PlayerCharacterAttackState))
        {
            return;
        }
        else
        {
            playerAinmaton.BackStepAnimation();

            playerRigidbody.velocity = -backTargetVector * 250 * Time.deltaTime;
        }
    }

    public void Skill1()
    {
        isSwapAble = false;
        rangeSkill.UseSkill(playerAinmaton.animator);
        playerAinmaton.SkillAnimation("Skill" + skillNum.ToString());

        if (swapCoroutine != null)
        {
            StopCoroutine(SwapWaitTime(0.0f));
        }
        swapCoroutine = StartCoroutine(SwapWaitTime(3.0f));
    }

    public void AttackEnd() //공격 모션이 종료될 때 상태가 바뀝니다.
    {
        Debug.Log("공격 종료 호출");

        PlayerState = new PlayerCharacterIdleState(this);

        if (swapCoroutine != null)
        {
            StopCoroutine(SwapWaitTime(0.0f));
        }
        swapCoroutine = StartCoroutine(SwapWaitTime(3.0f));  
    }

    public void BackStepDone() //회피 모션이 종료될 때 상태가 바뀝니다.
    {
        Debug.Log("백스텝 종료 호출");

        PlayerState = new PlayerCharacterIdleState(this);

        if (swapCoroutine != null)
        {
            StopCoroutine(SwapWaitTime(0.0f));
        }
        swapCoroutine = StartCoroutine(SwapWaitTime(3.0f));

    }

    public void WeaponSwitching(PlayerCharacterWeaponState NewWeaponState)
    {
        if(CurrentWeaponState == NewWeaponState)
        {
            return;
        }
        else if(CurrentWeaponState != NewWeaponState && isSwapAble == true)
        {
            playerAinmaton.WeaponSwap(NewWeaponState);
            Debug.Log(PlayerState);
            Debug.Log(isSwapAble);
            weaponSwap.SetWeapon(true, NewWeaponState, CurrentWeaponState);

            CurrentWeaponState = NewWeaponState;
        }
    }
    public IEnumerator SwapWaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        isSwapAble = true;      
    }

}
public enum PlayerCharacterWeaponState
{
    ShortSword,
    LongSword,
}


