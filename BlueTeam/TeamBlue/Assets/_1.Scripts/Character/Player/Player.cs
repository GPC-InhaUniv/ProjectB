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

    public class Player : Character , IInitializable
    {
        Vector3 targetVector;
        public Vector3 TargetVector { get { return targetVector; } }

        float maxExp;
        public float MaxExp { get { return maxExp; } private set { } }

        [SerializeField]
        bool isDied = false;

        [SerializeField]
        bool isRunning = false;
        public bool IsRunning { get { return isRunning; } private set { } }
      
        [SerializeField]
        bool isWorking = false;
        public bool IsWorking { get { return isWorking; } private set{ } }

        Collider collider;
        public Collider Collider { get { return collider; } }

        PlayerAnimation playerAinmaton;
        public PlayerAnimation PlayerAinmaton { get { return playerAinmaton; } }

        Rigidbody playerRigidbody;
        public Rigidbody PlayerRigidbody { get { return playerRigidbody; } }
        
        PlayerPresenter playerPresenter;
        Weapon weapon;

        public Vector3 MoveVector;

        public PlayerCharacterState PlayerState;
        
        public PlayerCharacterWeaponState CurrentWeaponState;    
         
        float preAttckPower;
        int equipmentHp = 0;
        int equipmentAttackPower = 0;
        int equipmentDefensePowr = 0;

        bool isRunningHitCoroutine = false;

        [SerializeField]
        GameObject hitParticle;

        private void Awake()
        {
            //playerPresenter = GameObject.FindGameObjectWithTag("PlayerPresenter").GetComponent<PlayerPresenter>();
           // TestSetCharacterStatus();
            //상단 두줄은 테스트용임, 오류날 시 주석처리 

            playerRigidbody = GetComponent<Rigidbody>();
            weapon = GetComponent<Weapon>();
            playerAinmaton = GetComponent<PlayerAnimation>();
            collider = GetComponent<CapsuleCollider>();

            ChangeState(PlayerStates.PlayerCharacterIdleState);
            CurrentWeaponState = PlayerCharacterWeaponState.ShortSword;
        }

        void Start()
        {
            //playerPresenter.SetpUpUI();
            //상단 줄은 테스트용임, 오류날 시 주석처리 

            hitParticle.SetActive(false);

            targetVector = new Vector3(0, 0, 1);            
        }

        //테스트용 함수 - 삭제 예정
        void TestSetCharacterStatus()
        {
            exp = 200;
            level = 1;

            maxHealthPoint = Level * 1000 + equipmentHp;
            healthPoint = maxHealthPoint;

            attackPower = level * 10;
            defensivePower = equipmentDefensePowr;

            maxExp = 1000 + (100 * 1.2f * level);
            preAttckPower = attackPower + equipmentAttackPower;
        }
        //테스트용 함수 - 삭제 예정

        public void Initialize()
        {
            isDied = false;
            playerAinmaton.ResetHitTrigger();

            ChangeState(PlayerStates.PlayerCharacterIdleState);

            weapon.SetShortSword();
            playerAinmaton.InitStateAnimation();
            playerAinmaton.InitWeapon();

            playerPresenter = GameObject.FindGameObjectWithTag("PlayerPresenter").GetComponent<PlayerPresenter>();
           
            GetCharacterStatusFromDataManager();
            SetCharacterStatus();

            playerPresenter.SetpUpUI();
        }

        void Update()
        {
            if (MoveVector != Vector3.zero)
            {
                targetVector = MoveVector;
            }

            playerAinmaton.RunAnimation(isRunning);
        }

        public void SetState(PlayerCharacterState newState)
        {
            if(isDied == true)
            {
                return;
            }
            if (PlayerState == newState)
            {
                return;
            }
            else
            {
                PlayerState = newState;
                PlayerState.Tick(MoveVector);
            }
        }

        public void ChangeState(PlayerStates playerState)
        {
            switch (playerState)
            {
                case PlayerStates.PlayerCharacterIdleState:
                    isRunning = false; isWorking = false;
                    SetAttackPower(1.0f);
                    SetState(new PlayerCharacterIdleState(PlayerAinmaton, PlayerRigidbody, transform, Collider, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterAttackState:
                    isWorking = true;
                    SetAttackPower(1.2f);
                    SetState(new PlayerCharacterAttackState(PlayerAinmaton, PlayerRigidbody, transform, Collider, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterSkillState:
                    isWorking = true;
                    SetAttackPower(3.0f);
                    SetState(new PlayerCharacterSkillState(PlayerAinmaton, PlayerRigidbody, transform, Collider, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterBackStepState:
                    isWorking = true;
                    SetState(new PlayerCharacterBackStepState(PlayerAinmaton, PlayerRigidbody, transform, Collider, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterRunState:
                    isRunning = true;
                    SetState(new PlayerCharacterRunState(PlayerAinmaton, PlayerRigidbody, transform, Collider, TargetVector));
                    break;            
                case PlayerStates.PlayerCharacterDieState:
                    SetState(new PlayerCharacterDieState(PlayerAinmaton, PlayerRigidbody, transform, Collider, TargetVector));
                    isDied = true;
                    break;
                default:
                    break;
            }
        }

        void SetAttackPower(float power) 
        {
            attackPower = preAttckPower  * power;
        }

        public override void ReceiveDamage(float damage)
        {
            if (isWorking == true && isRunning == true && isDied == true)
                return;

            if (healthPoint <= 0)
            {
                healthPoint = 0;
                ChangeState(PlayerStates.PlayerCharacterDieState);

                SoundManager.Instance.SetSound(SoundFXType.PlayerDeath);

                GameControllManager.Instance.CheckGameOver();
                playerPresenter.UpdateHpUI();
            }

            if (isRunningHitCoroutine == false)
            {
                ChangeState(PlayerStates.PlayerCharacterIdleState);
                StartCoroutine(HitCoroutine(1.2f));

                playerAinmaton.HitAnimation();
                healthPoint -= CalDamage(damage);

                SoundManager.Instance.SetSound(SoundFXType.PlayerHit);
                playerPresenter.UpdateHpUI();
            }
        }

        float CalDamage(float damage)
        {
            float trueDamage = damage - defensivePower * 0.1f * 0.5f;

            if (0 < trueDamage)
            {
                return trueDamage;
            }
            else return 0;
        }

        public void WeaponSwitching(PlayerCharacterWeaponState NewWeaponState)
        {
            if (CurrentWeaponState == NewWeaponState)
            {
                return;
            }
            else if (IsRunning == false)
            {
                playerAinmaton.WeaponSwapAnimation(NewWeaponState);
                weapon.SetWeapon(true, NewWeaponState, CurrentWeaponState);
           
                CurrentWeaponState = NewWeaponState;
            }
        }

        //데이터 불러오기 및 정리하기
        void GetCharacterStatusFromDataManager()
        {
            GameDataManager.Instance.GetEquipmentStatus(out equipmentHp, out equipmentAttackPower, out equipmentDefensePowr);
            exp = GameDataManager.Instance.PlayerInfomation.PlayerExp;
            level = GameDataManager.Instance.PlayerInfomation.PlayerLevel;
        }

        void SetCharacterStatus()
        {
            maxHealthPoint = level * 1000 + equipmentHp;
            healthPoint = maxHealthPoint;
        
            maxExp = (100 * 1.2f * level);

            attackPower = level * 10;
            preAttckPower = attackPower + equipmentAttackPower;

            defensivePower = equipmentDefensePowr;
        }
        //데이터 불러오기 및 정리하기

        //애니메이션 이벤트
        public void AttackStart()
        {
            SoundManager.Instance.SetSound(SoundFXType.PlayerAttack);
        }
        public void AttackDone()
        {
            ChangeState(PlayerStates.PlayerCharacterIdleState);
        }

        public void BackStepStart()
        {
            //추후 확장 가능성
        }

        public void BackStepEnd()
        {
            collider.enabled = true;
            ChangeState(PlayerStates.PlayerCharacterIdleState);
        }
        //애니메이션 이벤트

        IEnumerator HitCoroutine(float time)
        {
            isRunningHitCoroutine = true;

            hitParticle.SetActive(true);

            yield return new WaitForSeconds(time);

            hitParticle.SetActive(false);

            isRunningHitCoroutine = false;
        }



    }


}

