using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour,IDamageInteractionable, IPositionInteractionable
{
    PlayerAnimation playerAinmaton;
    Rigidbody playerRigidbody;

    public Vector3 moveVector;

    public bool isRunning;
    public bool isAttacking;
    public bool isBackStepping;
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

    PlayerStatus playerStatus;
    void Start()
    {
        attackNum = 1;
        backTargetVector = new Vector3(0, 0, 1);
        PlayerState = new PlayerCharacterIdleState(this);
        CurrentWeaponState = PlayerCharacterWeaponState.ShortSword;
        
        isSwapAble = true;

        playerStatus = GetComponent<PlayerStatus>();
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
        playerAinmaton.AttackAnimation("Attack" + attackNum.ToString());
    }

    public void Running(Vector3 moveVector)
    {
        playerRigidbody.velocity = moveVector * 450 * Time.deltaTime;
    }

    public void Turn(Vector3 moveVector)
    {
        transform.rotation = Quaternion.LookRotation(moveVector);
    }
    
    public void BackStep()
    {
        playerAinmaton.BackStepAnimation();
        playerRigidbody.velocity = -backTargetVector * 350 * Time.deltaTime;
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
        isAttacking = false;

        SetState(new PlayerCharacterIdleState(this));

        if (swapCoroutine != null)
        {
            StopCoroutine(SwapWaitTime(0.0f));
        }
        swapCoroutine = StartCoroutine(SwapWaitTime(2.0f));  
    }
    public void BackStepStart()
    {
        //무적 처리
    }

    public void BackStepEnd() //회피 모션이 종료될 때 상태가 바뀝니다.
    {
        SetState(new PlayerCharacterIdleState(this));

        if (swapCoroutine != null)
        {
            StopCoroutine(SwapWaitTime(0.0f));
        }
        swapCoroutine = StartCoroutine(SwapWaitTime(2.0f));

    }

    public void WeaponSwitching(PlayerCharacterWeaponState NewWeaponState)
    {
        if(CurrentWeaponState == NewWeaponState)
        {
            return;
        }
        else if(isSwapAble == true && isAttacking == false && isRunning == false && isBackStepping == false)
        {
            playerAinmaton.WeaponSwap(NewWeaponState);

            weaponSwap.SetWeapon(true, NewWeaponState, CurrentWeaponState);

            CurrentWeaponState = NewWeaponState;
        }
    }
    public IEnumerator SwapWaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        isSwapAble = true;      
    }
    public IEnumerator ChangeIdleState(float time)
    {
        yield return new WaitForSeconds(time);
        SetState(new PlayerCharacterIdleState(this));
    }

    public void SendDamage(IDamageInteractionable target) 
    {
        Test_Mediator.Instance.SendTarget(target, playerStatus.PlayerAttackPower);
    }

    public void ReceiveDamage(int damage)
    {
        playerAinmaton.HitAnimation();
        playerStatus.PlayerHp -= damage;
        if (playerStatus.PlayerHp <= 0)
        {
            playerStatus.PlayerHp = 0;
            //죽은 상태
        }
    }

    public void SendPosition()
    {
        throw new NotImplementedException();
    }

    public void ReceivePosition(Vector3 position)
    {
        throw new NotImplementedException();
    }

}
public enum PlayerCharacterWeaponState
{
    ShortSword,
    LongSword,
}


