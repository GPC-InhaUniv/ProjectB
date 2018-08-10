using System.Collections;
using UnityEngine;
using ProjectB.Character;


public class Player : Character
{

    PlayerAnimation playerAinmaton;
    Rigidbody playerRigidbody;

    [SerializeField]
    Vector3 targetVector;

    Weapon Weapon;

    Coroutine swapCoroutine;   

    public Vector3 MoveVector;

    public bool IsRunning;
    public bool IsSwapAble;

    public Collider collider;

    public int AttackNumber;

    public PlayerCharacterWeaponState CurrentWeaponState;
    public PlayerCharacterState PlayerState;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        Weapon = GetComponent<Weapon>();
        playerAinmaton = GetComponent<PlayerAnimation>();
        collider = GetComponent<CapsuleCollider>();
        PlayerState = new PlayerCharacterIdleState(this);

    }
    void Start()
    {

        AttackNumber = 1;

        targetVector = new Vector3(0, 0, 1);

        CurrentWeaponState = PlayerCharacterWeaponState.ShortSword;
        
        IsSwapAble = true;

        playerAinmaton.InitWeapon();
    }

    private void FixedUpdate()
    {       
        playerAinmaton.RunAnimation(IsRunning);
        if (MoveVector != Vector3.zero)
        {
            targetVector = MoveVector;
        }        
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
            PlayerState.Tick(MoveVector);
        }

    }

    public void PlayerAttack()
    {
        playerAinmaton.AttackAnimation(AnimationState.Attack.ToString() + AttackNumber.ToString());
    }


    public void Running(Vector3 MoveVector)
    {
        if(MoveVector != Vector3.zero)
        {
            playerRigidbody.velocity = MoveVector * 450 * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(MoveVector);
        }
    }

    public void BackStep()
    {
        playerAinmaton.BackStepAnimation();
        playerRigidbody.AddForce(-targetVector * 500 * Time.deltaTime, ForceMode.Impulse);
    }



    public void Skill()
    {
        //스킬 수치 어떻게 전달?

        //int preAttackPow = CharacterAttackPower;
        //CharacterAttackPower = CharacterAttackPower + 100;
         
        IsSwapAble = false;
        //스킬 끝나고 idle 돌아오게 하기
        playerRigidbody.AddForce(targetVector * 800 * Time.deltaTime, ForceMode.Impulse);
        playerAinmaton.SkillAnimation(AnimationState.Skill + 1.ToString());

        if (swapCoroutine != null)
        {
            StopCoroutine(SwapWaitTime(0.0f));
        }
        swapCoroutine = StartCoroutine(SwapWaitTime(3.0f));

        //CharacterAttackPower = preAttackPow;
    }

    public void WeaponSwitching(PlayerCharacterWeaponState NewWeaponState)
    {
        if(CurrentWeaponState == NewWeaponState)
        {
            return;
        }
        else if(IsSwapAble == true &&IsRunning == false)
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

    public void Die()
    {
        Debug.Log("죽음");
        playerAinmaton.DieAnimation();
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

    public override void SaveValue(int value)
    {
        CharacterExp = CharacterExp + value;
    }

    ////////////////////////애니메이션 이벤트
    public void AttackEnd()
    {
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

    public void BackStepEnd()
    {
        SetState(new PlayerCharacterIdleState(this));

        if (swapCoroutine != null)
        {
            StopCoroutine(SwapWaitTime(0.0f));
        }
        swapCoroutine = StartCoroutine(SwapWaitTime(2.0f));

    }

}

public enum PlayerCharacterWeaponState
{
    ShortSword,
    LongSword,
}


