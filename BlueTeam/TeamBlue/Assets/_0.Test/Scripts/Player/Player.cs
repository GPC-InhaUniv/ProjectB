using System.Collections;
using UnityEngine;
using ProjectB.GameManager;
using ProjectB.UI.Presenter;

namespace ProjectB.Characters.Players
{
    public enum PlayerCharacterWeaponState
    {
        ShortSword,
        LongSword,
    }
    public enum PlayerStates
    {
        PlayerCharacterIdleState,
        PlayerCharacterAttackState,
        PlayerCharacterSkillState,
        PlayerCharacterRunState,
        PlayerCharacterBackStepState,
        PlayerCharacterDieState
    }

    public class Player : Character
    {
        public PlayerPresenter PlayerPresenter { get { return playerPresenter; } }
        PlayerPresenter playerPresenter;

        public PlayerAnimation PlayerAinmaton { get { return playerAinmaton; } }
        PlayerAnimation playerAinmaton;

        public Rigidbody PlayerRigidbody { get { return playerRigidbody; } }
        Rigidbody playerRigidbody;

        public Collider Collider { get { return collider; } }
        Collider collider;
     
        public Vector3 TargetVector { get { return targetVector; } }
        Vector3 targetVector;

        public int AttackNumber { get { return attackNumber; } private set { } }
        int attackNumber;

        public Vector3 MoveVector;

        public bool IsRunning;
  
        public PlayerCharacterWeaponState CurrentWeaponState;

        public PlayerCharacterState PlayerState;

        float preAttckPower;

        bool IsDied;

        Weapon Weapon;

        private void Awake()
        {
            playerPresenter = GameObject.FindGameObjectWithTag("PlayerPresenter").GetComponent<PlayerPresenter>();
            playerRigidbody = GetComponent<Rigidbody>();
            Weapon = GetComponent<Weapon>();
            playerAinmaton = GetComponent<PlayerAnimation>();
            collider = GetComponent<CapsuleCollider>();

            ChangeState(PlayerStates.PlayerCharacterIdleState);

            CurrentWeaponState = PlayerCharacterWeaponState.ShortSword;

            GetCharacterStatusFromDataManager();
        }
        void Start()
        {
            targetVector = new Vector3(0, 0, 1);

            SetCharacterStatus();

            playerPresenter.ShowHUD();

            playerAinmaton.InitWeapon();
        }

        private void Update()
        {
            if (MoveVector != Vector3.zero)
            {
                targetVector = MoveVector;
            }
            playerAinmaton.RunAnimation(IsRunning);
        }

        public void SetState(PlayerCharacterState state)
        {
            if(IsDied == true)
            {
                return;
            }
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

        public void ChangeState(PlayerStates playerState)
        {
            switch (playerState)
            {
                case PlayerStates.PlayerCharacterIdleState:
                    SetAttackPower(1.0f);
                    SetState(new PlayerCharacterIdleState(PlayerAinmaton, PlayerRigidbody, transform, Collider, AttackNumber, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterAttackState:
                    SetAttackPower(1.2f);
                    SetState(new PlayerCharacterAttackState(PlayerAinmaton, PlayerRigidbody, transform, Collider, AttackNumber, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterSkillState:
                    SetAttackPower(2.0f);
                    SetState(new PlayerCharacterSkillState(PlayerAinmaton, PlayerRigidbody, transform, Collider, AttackNumber, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterRunState:
                    SetState(new PlayerCharacterRunState(PlayerAinmaton, PlayerRigidbody, transform, Collider, AttackNumber, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterBackStepState:
                    SetState(new PlayerCharacterBackStepState(PlayerAinmaton, PlayerRigidbody, transform, Collider, AttackNumber, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterDieState:
                    SetState(new PlayerCharacterDieState(PlayerAinmaton, PlayerRigidbody, transform, Collider, AttackNumber, TargetVector));
                    break;
                default:
                    break;
            }
        }

        public void WeaponSwitching(PlayerCharacterWeaponState NewWeaponState)
        {            
            if (CurrentWeaponState == NewWeaponState)
            {
                return;
            }
            else if (IsRunning == false)
            {
                playerAinmaton.Weapon(NewWeaponState);

                Weapon.SetWeapon(true, NewWeaponState, CurrentWeaponState);

                CurrentWeaponState = NewWeaponState;
            }
        }
        public void SetAttackNumber(int number)
        {
            attackNumber = number;
        }

        void SetAttackPower(float power) 
        {
            characterAttackPower = preAttckPower  * power;
        }

        public override void ReceiveDamage(float damage)
        {
            if(IsDied == false)
            {
                StartCoroutine(HitCoroutine(1.0f));
                playerAinmaton.HitAnimation();
                characterHealthPoint -= CalDamage(damage);
                playerPresenter.ShowHUD();
            }

            if (CharacterHealthPoint <= 0)
            {
                IsDied = true;
                characterHealthPoint = 0;
                ChangeState(PlayerStates.PlayerCharacterDieState);
                //게임 컨트롤러에 게임 오버 요청할 것
            }
        }

        float CalDamage(float damage)
        {
            if (0 < damage - characterDefensivePower / 10)
            {
                return damage - characterDefensivePower * 0.1f;
            }
            else return 0;
        }

        //데이터 불러오기 및 정리하기
        void GetCharacterStatusFromDataManager()
        {
            //test
            characterExp = 57.2f;
            characterLevel = 1;
            characterDefensivePower = 50;
            //test

            //장착한 아이템의 공격력과 방어력, 체력 증가력 불러오기 추가

            //characterExp = GameDataManager.Instance.PlayerInfomation.PlayerExp;
            //characterLevel = GameDataManager.Instance.PlayerInfomation.PlayerLevel;
        }

        void SetCharacterStatus()
        {
            // /장착한 아이템의 공격력과 방어력, 체력 증가력을 스탯에 적용하기
            //곱한 계수는 임시입니다.
            characterMaxHealthPoint = characterLevel * 100;
            characterHealthPoint = characterMaxHealthPoint;
            characterAttackPower = characterLevel * 10;

            preAttckPower = characterAttackPower;
        }
        //데이터 불러오기 및 정리하기

        //애니메이션 이벤트
        public void AttackDone()
        {
            ChangeState(PlayerStates.PlayerCharacterIdleState);
        }

        public void BackStepStart()
        {
            //collider.enabled = false;
        }

        public void BackStepEnd()
        {
            collider.enabled = true;
            ChangeState(PlayerStates.PlayerCharacterIdleState);
        }
        //애니메이션 이벤트

        IEnumerator HitCoroutine(float time)
        {
            collider.enabled = false;
            yield return new WaitForSeconds(time);
            collider.enabled = true;
        }
    }


}

