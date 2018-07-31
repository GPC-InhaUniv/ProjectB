using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    PlayerAnimation playerAinmaton;
    Rigidbody PCrigidbody;

    public Vector3 DirectionVector; //플레이의 캐릭터 정면이 바라보는 방향

    public Vector3 moveVector;
    public bool isRunning;
    public bool isBackStep;
    public int attackNum;

    public PlayerCharacterWeaponState CurrentweaponState;
    public PlayerCharacterState PcState;

    [SerializeField]
    Vector3 BackTargetVector;

    WeaponSwap weaponSwap;
    void Start()
    {
        BackTargetVector = new Vector3(0, 0, 1);

        PcState = new PlayerCharacterIdleState(this);
        CurrentweaponState = PlayerCharacterWeaponState.ShortSword;

        playerAinmaton = GetComponent<PlayerAnimation>();
        PCrigidbody = GetComponent<Rigidbody>();
        weaponSwap = GetComponent<WeaponSwap>();


        playerAinmaton.InitWeapon();
    }

    private void FixedUpdate()
    {

        //뒤로 가는 벡터 방향 저장
        if (moveVector != Vector3.zero)
        {
            BackTargetVector = moveVector;
        }

        PcState.Tick(moveVector);
        ActionRun();
    }

    public void SetState(PlayerCharacterState state)
    {
        if (PcState == state)
        {
            return;
        }
        else
        {
            PcState = state;
        }

    }

    public void PlayerAttack()
    {
        playerAinmaton.AttackAnimation("Attack" + attackNum.ToString());
    }

    public void RunToPC(Vector3 moveVector)
    {
        if(PcState.GetType() == typeof(PlayerCharacterAttackState))
        {
            return;
        }
        else
        {
            PCrigidbody.velocity = moveVector * 450 * Time.deltaTime;
        }
    }
    public void ActionRun()
    {
        playerAinmaton.RunAnimation(isRunning);
    }

    public void TurnToPC(Vector3 moveVector)
    {
        transform.rotation = Quaternion.LookRotation(moveVector);
    }
    
    public void BackStepToPC()
    {
        if (PcState.GetType() == typeof(PlayerCharacterAttackState))
        {
            return;
        }
        else
        {
            playerAinmaton.BackStepAnimation();

            PCrigidbody.velocity = -BackTargetVector * 250 * Time.deltaTime;
        }
    }


    public void AttackDone() //공격 모션이 종료될 때 상태가 바뀝니다.
    {
        PcState = new PlayerCharacterIdleState(this);
    }

    public void BackStepDone() //회피 모션이 종료될 때 상태가 바뀝니다.
    {
        PcState = new PlayerCharacterIdleState(this);
    }

    public void WeaponSwitching(PlayerCharacterWeaponState NewWeaponState)
    {
        if(CurrentweaponState == NewWeaponState)
        {
            return;
        }
        else if(CurrentweaponState != NewWeaponState && PcState.GetType() == typeof(PlayerCharacterIdleState))
        {
            playerAinmaton.WeaponSwap(NewWeaponState);

            weaponSwap.SetWeapon(true, NewWeaponState, CurrentweaponState);

            CurrentweaponState = NewWeaponState;
        }
    }

}
public enum PlayerCharacterWeaponState
{
    ShortSword,
    LongSword,
}


