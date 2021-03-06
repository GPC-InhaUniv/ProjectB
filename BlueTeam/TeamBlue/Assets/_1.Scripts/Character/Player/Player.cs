﻿using System.Collections;
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

    public class Player : Character, IInitializable
    {
        [SerializeField]
        bool isDied;
        [SerializeField]
        bool isinvincible;

        float maxExp;
        public float MaxExp { get { return maxExp; } private set { } }

        [SerializeField]
        bool isRunning;
        public bool IsRunning { get { return isRunning; } private set { } }

        [SerializeField]
        bool isWorking;
        public bool IsWorking { get { return isWorking; } private set { } }

        PlayerAnimation playerAinmatons;
        public PlayerAnimation PlayerAinmatons { get { return playerAinmatons; } private set { } }

        Rigidbody playerRigidbody;
        public Rigidbody PlayerRigidbody { get { return playerRigidbody; } private set { } }

        PlayerCharacterWeaponState currentWeaponState;
        public PlayerCharacterWeaponState CurrentWeaponState { get { return currentWeaponState; } private set { } }

        Weapon weapon;

        PlayerCharacterState currentPlayerState;

        PlayerPresenter playerPresenter;

        public Vector3 MoveVector;

        float totoalAttckPower;

        int equipmentHp, equipmentAttackPower, equipmentDefensePowr;

        bool isRunningHitCoroutine, isSwapAble;

        [SerializeField]
        GameObject hitParticle;

        private void Awake()
        {
            weapon = GetComponent<Weapon>();
            playerRigidbody = GetComponent<Rigidbody>();
            playerAinmatons = GetComponent<PlayerAnimation>();

            currentWeaponState = PlayerCharacterWeaponState.ShortSword;

            currentPlayerState = new PlayerCharacterIdleState(playerAinmatons, playerRigidbody, transform);
        }


        //void Start()
        //{
        //    weapon.SetShortSword();

        //    isDied = false;
        //    playerAinmatons.InitStateAnimation();

        //    hitParticle.SetActive(false);

        //    MoveVector = new Vector3(0, 0, 1);

        //    if (currentWeaponState != PlayerCharacterWeaponState.ShortSword)
        //    {
        //        WeaponSwitching(PlayerCharacterWeaponState.ShortSword);
        //        weapon.SetShortSword();
        //    }

        //    playerPresenter = GameObject.FindGameObjectWithTag("PlayerPresenter").GetComponent<PlayerPresenter>();

        //    TestSetCharacterStatus();

        //    playerPresenter.SetpUpUI();
        //    //상단 줄은 테스트용임, 오류날 시 주석처리        
        //}

        ////테스트용 함수 - 삭제 예정
        //void TestSetCharacterStatus()
        //{
        //    exp = 200;
        //    level = 1;

        //    maxHealthPoint = Level * 1000 + equipmentHp;
        //    healthPoint = maxHealthPoint;

        //    attackPower = level * 10;
        //    defensivePower = equipmentDefensePowr;

        //    maxExp = 1000 + (100 * 1.2f * level);
        //    totoalAttckPower = attackPower + equipmentAttackPower;
        //}
        ////테스트용 함수 - 삭제 예정


        public void Initialize()
        {
            isDied = false;

            hitParticle.SetActive(false);

            MoveVector = new Vector3(0, 0, 1);

            playerAinmatons.InitStateAnimation();

            weapon.SetShortSword();

            if (currentWeaponState != PlayerCharacterWeaponState.ShortSword)
            {
                WeaponSwitching(PlayerCharacterWeaponState.ShortSword);
                weapon.SetShortSword();
            }
           
            if (playerPresenter == null)
            {
                playerPresenter = GameObject.FindGameObjectWithTag("PlayerPresenter").GetComponent<PlayerPresenter>();
            }
           
            GetCharacterStatusFromDataManager();
            SetCharacterStatus();

            playerPresenter.SetpUpUI();
        }

        //데이터 불러오기 및 정리
        void GetCharacterStatusFromDataManager()
        {
            exp = GameDataManager.Instance.PlayerInfomation.PlayerExp;
            level = GameDataManager.Instance.PlayerInfomation.PlayerLevel;
            GameDataManager.Instance.GetEquipmentStatus(out equipmentAttackPower, out equipmentHp, out equipmentDefensePowr);
        }
        void SetCharacterStatus()
        {
            maxHealthPoint = level * 500 + equipmentHp;
            healthPoint = maxHealthPoint;

            maxExp = 1000 + (100 * 1.2f * level);

            attackPower = level * 100;

            totoalAttckPower = attackPower + equipmentAttackPower;
            defensivePower = equipmentDefensePowr;
        }

        public void SetState(PlayerCharacterState newState)
        {
            if (isDied == true) return;

            if (currentPlayerState == newState) return;

            else
            {
                currentPlayerState = newState;
                currentPlayerState.Tick(MoveVector, isRunning);
            }
        }

        public void ChangeState(PlayerStates playerStates)
        {
            switch (playerStates)
            {
                case PlayerStates.PlayerCharacterIdleState:
                    isRunning = false;
                    SetAttackPower(1.0f);
                    currentPlayerState.Tick(MoveVector, isRunning);
                    SetState(new PlayerCharacterIdleState(playerAinmatons, playerRigidbody, transform));
                    break;
                case PlayerStates.PlayerCharacterAttackState:
                    isWorking = true;
                    SetAttackPower(1.2f);
                    SetState(new PlayerCharacterAttackState(playerAinmatons, playerRigidbody, transform));
                    break;
                case PlayerStates.PlayerCharacterSkillState:
                    isWorking = true;
                    SetAttackPower(3.0f);
                    SetState(new PlayerCharacterSkillState(playerAinmatons, playerRigidbody, transform));
                    break;
                case PlayerStates.PlayerCharacterBackStepState:
                    isinvincible = true;
                    SetState(new PlayerCharacterBackStepState(playerAinmatons, playerRigidbody, transform));
                    break;
                case PlayerStates.PlayerCharacterRunState:
                    isRunning = true;
                    SetState(new PlayerCharacterRunState(playerAinmatons, playerRigidbody, transform));
                    break;
                case PlayerStates.PlayerCharacterDieState:
                    SetState(new PlayerCharacterDieState(playerAinmatons, playerRigidbody, transform));
                    isDied = true;
                    break;
                default:
                    break;
            }
        }

        void SetAttackPower(float power)
        {
            attackPower = totoalAttckPower * power;
        }

        public override void ReceiveDamage(float damage)
        {
            if (isRunning == true || isinvincible == true || isDied == true)
                return;

            if (isRunningHitCoroutine == false)
            {
                isWorking = false;
                SetState(new PlayerCharacterIdleState(playerAinmatons, playerRigidbody, transform));

                StartCoroutine(HitCoroutine(1.2f));

                SoundManager.Instance.SetSound(SoundFXType.PlayerHit);
                playerAinmatons.HitAnimation();
                healthPoint -= CalDamage(damage);

                playerPresenter.UpdateHpUI();

                if (healthPoint <= 0)
                {
                    healthPoint = 0;
                    playerPresenter.UpdateHpUI();

                    ChangeState(PlayerStates.PlayerCharacterDieState);

                    SoundManager.Instance.SetSound(SoundFXType.PlayerDeath);

                    GameControllManager.Instance.CheckGameOver();
                }
            }
        }

        float CalDamage(float damage)
        {
            float trueDamage = damage - (defensivePower * 0.05f);

            if (0 < trueDamage)
            {
                return trueDamage;
            }
            else return 1;
        }

        public void WeaponSwitching(PlayerCharacterWeaponState NewWeaponState)
        {
            if (currentWeaponState == NewWeaponState) return;

            if(isSwapAble != true)
            {
                StartCoroutine(IsinvincibleTimeDuringSwap(0.5f));

                playerAinmatons.WeaponSwapAnimation(NewWeaponState);

                weapon.SetWeapon(true, NewWeaponState, currentWeaponState);

                currentWeaponState = NewWeaponState;
            }
        }

        //애니메이션 이벤트
        public void AttackStart()
        {
            SoundManager.Instance.SetSound(SoundFXType.PlayerAttack);
        }
        public void AttackDone()
        {
            isWorking = false;
        }
        public void BackStepStart()
        {       
            //백스텝 사운드 추가 예정
        }
        public void BackStepEnd()
        {
            isinvincible = false;
        }

        IEnumerator HitCoroutine(float time)
        {
            isRunningHitCoroutine = true;

            hitParticle.SetActive(true);

            yield return new WaitForSeconds(time);

            hitParticle.SetActive(false);

            isRunningHitCoroutine = false;
        }
        IEnumerator IsinvincibleTimeDuringSwap(float time)
        {
            isSwapAble = true;
            isinvincible = true;
            yield return new WaitForSeconds(time);
            isinvincible = false;
            isSwapAble = false;
        }
    }
}

