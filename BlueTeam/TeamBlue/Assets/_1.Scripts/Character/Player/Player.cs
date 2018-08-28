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

        public float MaxExp { get { return maxExp; } private set { } }
        float maxExp;

        public bool IsRunning { get { return isRunning; } private set { } }
        [SerializeField]
        bool isRunning = false;

        public bool IsWorking { get { return isWorking; } private set{ } }
        [SerializeField]
        bool isWorking = false;

        [SerializeField]
        bool isDied = false;

        public Vector3 MoveVector;

        public PlayerCharacterWeaponState CurrentWeaponState;

        public PlayerCharacterState PlayerState;

        float preAttckPower;

        Weapon weapon;

        int equipmentHp = 0;
        int equipmentAttackPower = 0;
        int equipmentDefensePowr = 0;

        [SerializeField]
        GameObject hitParticle;

        bool isRunningHitCoroutine = false;

        private void Awake()
        {
            //playerPresenter = GameObject.FindGameObjectWithTag("PlayerPresenter").GetComponent<PlayerPresenter>();
            //TestSetCharacterStatus();
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
            //playerPresenter.UpdateUI();
            //상단 줄은 테스트용임, 오류날 시 주석처리 

            hitParticle.SetActive(false);

            targetVector = new Vector3(0, 0, 1);
            
            playerAinmaton.InitWeapon();
        }

        void TestSetCharacterStatus() //테스트용 함수 - 삭제 예정
        {
            exp = 200;
            level = 1;

            maxHealthPoint = Level * 1000 + equipmentHp;
            healthPoint = maxHealthPoint;

            attackPower = level * 10;
            defensivePower += equipmentDefensePowr;

            maxExp = (1000 * 1.2f * level);
            preAttckPower = attackPower + equipmentAttackPower;
        }

        public void Initialize()
        {
            playerPresenter = GameObject.FindGameObjectWithTag("PlayerPresenter").GetComponent<PlayerPresenter>();
           
            GetCharacterStatusFromDataManager();
            SetCharacterStatus();
            playerPresenter.UpdateUI();
        }

        void Update()
        {
            if (MoveVector != Vector3.zero)
            {
                targetVector = MoveVector;
            }

            playerAinmaton.RunAnimation(isRunning);
        }

        void ReturnIdleState() //다시 살리는 함수
        {
            isDied = false;
            ChangeState(PlayerStates.PlayerCharacterIdleState);
            playerAinmaton.InitStateAnimation();
            playerAinmaton.InitWeapon();
            SetCharacterStatus();
            playerPresenter.UpdateUI();
        }

        public void SetState(PlayerCharacterState state)
        {
            if(isDied == true)
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
                    isRunning = false;
                    isWorking = false;
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
                case PlayerStates.PlayerCharacterRunState:
                    isRunning = true;
                    SetState(new PlayerCharacterRunState(PlayerAinmaton, PlayerRigidbody, transform, Collider, TargetVector));
                    break;
                case PlayerStates.PlayerCharacterBackStepState:
                    isWorking = true;
                    SetState(new PlayerCharacterBackStepState(PlayerAinmaton, PlayerRigidbody, transform, Collider, TargetVector));
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
            if (isWorking == true && isDied == true)
                return;
            if(isRunningHitCoroutine == false)
            {
                ChangeState(PlayerStates.PlayerCharacterIdleState);
                StartCoroutine(HitCoroutine(1.2f));

                playerAinmaton.HitAnimation();
                healthPoint -= CalDamage(damage);

                SoundManager.Instance.SetSound(SoundFXType.PlayerHit);
                playerPresenter.UpdateUI();
            }

            if (healthPoint <= 0)
            {
                healthPoint = 0;
                ChangeState(PlayerStates.PlayerCharacterDieState);
                GameControllManager.Instance.CheckGameOver();
                playerPresenter.UpdateUI();
            }
        }

        float CalDamage(float damage)
        {
            if (0 < damage - defensivePower * 0.1f)
            {
                return damage - defensivePower * 0.1f;
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
            GameDataManager.Instance.GetEquipmentStatus(ref equipmentHp,ref equipmentAttackPower,ref equipmentDefensePowr);
            exp = GameDataManager.Instance.PlayerInfomation.PlayerExp;
            level = GameDataManager.Instance.PlayerInfomation.PlayerLevel;
        }

        void SetCharacterStatus()
        {
            maxHealthPoint = level * 1000 + equipmentHp;
            healthPoint = maxHealthPoint;

            attackPower = level * 10;
            defensivePower += equipmentDefensePowr;

            maxExp = (1000 * 1.2f * level);
            preAttckPower = attackPower + equipmentAttackPower;
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

