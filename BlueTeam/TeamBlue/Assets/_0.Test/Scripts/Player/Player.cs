using System.Collections;
using UnityEngine;


public class Player : Character
{

    PlayerAnimation playerAinmaton;
    Rigidbody playerRigidbody;

    [SerializeField]
    Vector3 backTargetVector;

    Weapon Weapon;

    ISkillUsable rangeSkill;

    Coroutine swapCoroutine;   

    Vector3 moveVector;

    public bool IsRunning;
    public bool IsAttacking;
    public bool IsBackStepping;
    public bool IsSwapAble;

    public Collider collider;

    public int AttackNumber;
    public int SkillNumber;

    public PlayerCharacterWeaponState CurrentWeaponState;
    public PlayerCharacterState PlayerState;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        Weapon = GetComponent<Weapon>();
        playerAinmaton = GetComponent<PlayerAnimation>();
        collider = GetComponent<CapsuleCollider>();
        PlayerState = new PlayerCharacterIdleState(this);

        AttackNumber = 1;

        backTargetVector = new Vector3(0, 0, 1);


        CurrentWeaponState = PlayerCharacterWeaponState.ShortSword;
        
        IsSwapAble = true;


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
        else
            playerAinmaton.RunAnimation(IsRunning);

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
            PlayerState.Tick(moveVector);
        }

    }

    public void PlayerAttack()
    {
        playerAinmaton.AttackAnimation(AnimationState.Attack.ToString() + AttackNumber.ToString());
    }


    public void Running(Vector3 MoveVector)
    {
        playerAinmaton.RunAnimation(IsRunning);
        playerRigidbody.velocity = MoveVector * 450 * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(MoveVector);

    }

    public void BackStep()
    {
        playerAinmaton.BackStepAnimation();
        //playerRigidbody.velocity = -backTargetVector * 350 * Time.deltaTime;
        playerRigidbody.AddForce(-backTargetVector*450 * Time.deltaTime, ForceMode.Impulse);
    }



    public void Skill()
    {
        //int preAttackPow = CharacterAttackPower;
        //CharacterAttackPower = CharacterAttackPower + 100;
         
        IsSwapAble = false;

        rangeSkill.UseSkill();


        playerAinmaton.SkillAnimation(AnimationState.Skill + SkillNumber.ToString());

        if (swapCoroutine != null)
        {
            StopCoroutine(SwapWaitTime(0.0f));
        }
        swapCoroutine = StartCoroutine(SwapWaitTime(3.0f));

        //CharacterAttackPower = preAttackPow;
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
        //무적 구현
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
        else if(IsSwapAble == true && IsAttacking != true)
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


    public void ReceivePosition(Vector3 position)
    {
        //미니맵
    }

    public override void ReceiveDamage(int damage)
    {
        playerAinmaton.HitAnimation();
        CharacterHealthPoint -= damage;
        Debug.Log("플레이어 댐지 받음");
        if (CharacterHealthPoint <= 0)
        {
            CharacterHealthPoint = 0;
            SetState(new PlayerCharacterDieState(this));
        }
    }

    public void Die()
    {
        Debug.Log("죽음");
        playerAinmaton.DieAnimation();
    }

    //public override int SendValue()
    //{
    //    return CharacterExp;
    //}

    public override void SaveValue(int value)
    {
        CharacterExp = CharacterExp + value;
    }
}
public enum PlayerCharacterWeaponState
{
    ShortSword,
    LongSword,
}


