using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{

    PlayerAnimation playerAinmaton;
    Rigidbody playerRigidbody;

    [SerializeField]
    Vector3 backTargetVector;

    Weapon Weapon;
    PlayerStatus playerStatus;

    ISkillUsable rangeSkill;

    Coroutine swapCoroutine;   

    Vector3 moveVector;

    public bool IsRunning;
    public bool IsAttacking;
    public bool IsBackStepping;
    public bool IsSwapAble;

    public int AttackNumber;
    public int SkillNumber;

    public PlayerCharacterWeaponState CurrentWeaponState;
    public PlayerCharacterState PlayerState;



    void Start()
    {
        AttackNumber = 1;

        backTargetVector = new Vector3(0, 0, 1);

        PlayerState = new PlayerCharacterIdleState(this);
        CurrentWeaponState = PlayerCharacterWeaponState.ShortSword;
        
        IsSwapAble = true;

        playerStatus = GetComponent<PlayerStatus>();
        playerAinmaton = GetComponent<PlayerAnimation>();
        playerRigidbody = GetComponent<Rigidbody>();
        Weapon = GetComponent<Weapon>();

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

        playerAinmaton.RunAnimation(IsRunning);
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
        playerAinmaton.AttackAnimation("Attack" + AttackNumber.ToString());
    }

    public void Running(Vector3 MoveVector)
    {
        playerRigidbody.velocity = MoveVector * 450 * Time.deltaTime;
    }

    public void Turn(Vector3 MoveVector)
    {
        transform.rotation = Quaternion.LookRotation(MoveVector);

        //보간하기
    }

    public void BackStep()
    {
        playerAinmaton.BackStepAnimation();
        playerRigidbody.velocity = -backTargetVector * 350 * Time.deltaTime;
    }

    public void Die()
    {
        playerAinmaton.DieAnimation();
    }

    public void Skill1()
    {
        IsSwapAble = false;

        rangeSkill.UseSkill();
        playerAinmaton.SkillAnimation("Skill" + SkillNumber.ToString());

        if (swapCoroutine != null)
        {
            StopCoroutine(SwapWaitTime(0.0f));
        }
        swapCoroutine = StartCoroutine(SwapWaitTime(3.0f));
    }


    public void AttackEnd() //공격 모션이 종료될 때 상태가 바뀝니다.
    {
        IsAttacking = false;

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
    public void SetMoveVector(Vector3 inputVector)
    {
        moveVector = inputVector;
    }

    public void WeaponSwitching(PlayerCharacterWeaponState NewWeaponState)
    {
        if(CurrentWeaponState == NewWeaponState)
        {
            return;
        }
        else if(IsSwapAble == true && IsAttacking == false && IsRunning == false && IsBackStepping == false)
        {
            playerAinmaton.Weapon(NewWeaponState);

            Weapon.SetWeapon(true, NewWeaponState, CurrentWeaponState);

            CurrentWeaponState = NewWeaponState;
        }
    }
    public IEnumerator SwapWaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        IsSwapAble = true;      
    }
    public IEnumerator ChangeIdleState(float time)
    {
        yield return new WaitForSeconds(time);
        SetState(new PlayerCharacterIdleState(this));
    }

    /*
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
            SetState(new PlayerCharacterDieState(this));
        }
    }
    
    public void SendPosition()
    {
        Test_Mediator.Instance.SendPosition(transform.position);
    }*/

    public void ReceivePosition(Vector3 position)
    {
        
    }

}
public enum PlayerCharacterWeaponState
{
    ShortSword,
    LongSword,
}


