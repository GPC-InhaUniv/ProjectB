using System.Collections;
using UnityEngine;
using ProjectB.GameManager;

namespace ProjectB.Characters.Players
{

    public class Player : Character
    {

        PlayerAnimation playerAinmaton;
        Rigidbody playerRigidbody;

        [SerializeField]
        Vector3 targetVector;

        Weapon Weapon;

        public Vector3 MoveVector;

        public bool IsRunning;

        Collider collider;

        int attackNumber;

        public PlayerCharacterWeaponState CurrentWeaponState;
        public PlayerCharacterState PlayerState;


        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            Weapon = GetComponent<Weapon>();
            playerAinmaton = GetComponent<PlayerAnimation>();
            collider = GetComponent<CapsuleCollider>();

            PlayerState = new PlayerCharacterIdleState(this);
            GetCharacterStatusFromDataManager();
        }
        void Start()
        {
            SetCharacterStatus();

            targetVector = new Vector3(0, 0, 1);

            CurrentWeaponState = PlayerCharacterWeaponState.ShortSword;

            playerAinmaton.InitWeapon();
        }
        private void Update()
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
            playerAinmaton.AttackAnimation(AnimationState.Attack.ToString() + attackNumber.ToString());
        }

        public void Running(Vector3 MoveVector)
        {
            if (MoveVector != Vector3.zero)
            {
                playerRigidbody.velocity = MoveVector * 450 * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(MoveVector); //보간필요
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

            //스킬 끝나고 idle 돌아오게 하기
            playerRigidbody.AddForce(targetVector * 800 * Time.deltaTime, ForceMode.Impulse);
            playerAinmaton.SkillAnimation(AnimationState.Skill + 1.ToString());
            //CharacterAttackPower = preAttackPow;
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
        public void Die()
        {
            Debug.Log("플레이어 사망");
            playerAinmaton.DieAnimation();
            collider.enabled = false;
        }

        public override void ReceiveDamage(int damage)
        {
            playerAinmaton.HitAnimation();
            characterHealthPoint -= CalDamage(damage);
            Debug.Log(characterHealthPoint.ToString());
            if (CharacterHealthPoint <= 0)
            {
                characterHealthPoint = 0;
                SetState(new PlayerCharacterDieState(this));
            }
        }

        int CalDamage(int damage)
        {
            if (0 < damage - characterDefensivePower / 10)
            {
                return damage - characterDefensivePower / 10;
            }
            else return 0;
        }

        //데이터 불러오기 및 정리하기
        void GetCharacterStatusFromDataManager()
        {
            //test
            characterExp = 0;
            characterLevel = 1;
            characterDefensivePower = 100;
            //test

            //characterExp = GameDataManager.Instance.PlayerInfomation.PlayerExp;
            //characterLevel = GameDataManager.Instance.PlayerInfomation.PlayerLevel;
        }

        void SetCharacterStatus()
        {
            characterMaxHealthPoint = characterLevel * 100;
            characterHealthPoint = characterMaxHealthPoint;
            characterAttackPower = characterLevel * 10;
        }
        //데이터 불러오기 및 정리하기

        //애니메이션 이벤트
        public void AttackEnd()
        {
            SetState(new PlayerCharacterIdleState(this));
        }

        public void BackStepStart()
        {
            collider.enabled = false;
        }

        public void BackStepEnd()
        {
            collider.enabled = true;
            SetState(new PlayerCharacterIdleState(this));
        }
        //애니메이션 이벤트
    }

    public enum PlayerCharacterWeaponState
    {
        ShortSword,
        LongSword,
    }
}

