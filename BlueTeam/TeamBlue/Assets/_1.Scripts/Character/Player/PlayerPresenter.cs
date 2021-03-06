﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.Characters.Players;
using ProjectB.GameManager;
using ProjectB.Utility;

namespace ProjectB.UI.Presenter
{
    public class PlayerPresenter : MonoBehaviour , IInitializable
    {
        [SerializeField]
        Button attackButton, skillButton, backStepButton, weaponSwapButton;
        [SerializeField]
        Image attackImage, skillImage, backStepImage, weaponSwapImage;

        [SerializeField]
        Image hpBar,expBar;
        [SerializeField]
        Text level, hp, exp, ID;

        [SerializeField]
        JoyStick joyStick;

        Player player;

        CommandControll commandControll = new CommandControll();

        ICommand[] Attack = new ICommand[4];

        Vector3 inputMoveVector, moverDirection;

        float skillCoolDownTime, backStepCoolDownTime, swapCoolDownTime, attackCoolDownTime;

        float horizontal, vertical, expValue, hpValue;

        bool isSwap = false;

        int comboResetCount, comboRandom;

        const int standardPercent = 100;
     
        const int shortSwordAttackCount = 3;
        const int longSwordAttackCount = 2;

        void Start()
        {
           // Initialize();

            skillCoolDownTime = 4.0f;
            backStepCoolDownTime = 2.0f;
            swapCoolDownTime = 2.0f;
            attackCoolDownTime = 1.0f;

            comboRandom = Random.Range(1, 3);

            GetImage();           

            attackButton.onClick.AddListener(() => GenerateCombo());
            attackButton.onClick.AddListener(() => StartCombo());
            attackButton.onClick.AddListener(() => ShuffleCombo());

            skillButton.onClick.AddListener(() => InputSkillButton());

            backStepButton.onClick.AddListener(() => InputBackStep());

            weaponSwapButton.onClick.AddListener(() => InputWeaponSwapButton());
        }
       
        public void Initialize()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Attack[0] = new CommandAttack1(player.PlayerAinmatons);
            Attack[1] = new CommandAttack2(player.PlayerAinmatons);
            Attack[2] = new CommandAttack3(player.PlayerAinmatons);
            Attack[3] = new CommandAttack4(player.PlayerAinmatons);
        }

        void GetImage()
        {
            skillImage = skillButton.GetComponent<Image>();
            backStepImage = backStepButton.GetComponent<Image>();
            weaponSwapImage = weaponSwapButton.GetComponent<Image>();
            attackImage = attackButton.GetComponent<Image>();
        }

        void Update()
        {
            if (player == null) return;

            inputMoveVector = PoolInput();

            SetInputVector();
        }

        Vector3 PoolInput()
        {
            horizontal = joyStick.Horizontal();
            vertical = joyStick.Vertical();

            moverDirection = new Vector3(horizontal, 0, vertical).normalized;

            return moverDirection;
        }

        void SetInputVector()
        {
            if (inputMoveVector != Vector3.zero)
            {
                if (!player.IsWorking)
                {
                    player.MoveVector = inputMoveVector;
                    player.ChangeState(PlayerStates.PlayerCharacterRunState);
                }
            }
            else
            {
                player.ChangeState(PlayerStates.PlayerCharacterIdleState);
            }
        }

        void InputBackStep()
        {
            if (!player.IsRunning)
            {
                player.ChangeState(PlayerStates.PlayerCharacterBackStepState);

                StartButtonCoolDown(backStepCoolDownTime, backStepButton, backStepImage);

                SwapWeaponCoolDown(backStepCoolDownTime);
            }
        }

        void InputSkillButton()
        {
            if (!player.IsRunning)
            {
                player.ChangeState(PlayerStates.PlayerCharacterSkillState);

                StartButtonCoolDown(skillCoolDownTime, skillButton, skillImage);

                SwapWeaponCoolDown(skillCoolDownTime);
            }
        }

        void InputWeaponSwapButton()
        {
            if (!player.IsRunning)
            {
                if (GetWeaponState())
                {
                    player.WeaponSwitching(PlayerCharacterWeaponState.LongSword);
                }
                else
                {
                    player.WeaponSwitching(PlayerCharacterWeaponState.ShortSword);
                }

                SwapWeaponCoolDown(swapCoolDownTime);

                commandControll.ClearCommand();
                comboResetCount = 0;
            }
        }

        void GenerateCombo()
        {
            switch (comboRandom)
            {
                case 1:
                    ComboOne();
                    break;
                case 2:
                    ComboTwo();
                    break;
            }
        }

        void ComboOne()
        {
            if (comboResetCount > 0) return;
            if (GetWeaponState())
            {
                commandControll.TakeCommand(Attack[0]);
                commandControll.TakeCommand(Attack[1]);
                commandControll.TakeCommand(Attack[2]);
            }
            else
            {
                commandControll.TakeCommand(Attack[0]);
                commandControll.TakeCommand(Attack[1]);
            }
        }

        void ComboTwo()
        {
            if (comboResetCount > 0) return;

            if (GetWeaponState())
            {
                commandControll.TakeCommand(Attack[0]);
                commandControll.TakeCommand(Attack[1]);
                commandControll.TakeCommand(Attack[3]);
            }
            else
            {
                commandControll.TakeCommand(Attack[0]);
                commandControll.TakeCommand(Attack[2]);
            }

        }

        void StartCombo()
        {
            if (!player.IsRunning)
            {
                player.ChangeState(PlayerStates.PlayerCharacterAttackState);
                commandControll.ExcuteCommand();

                StartButtonCoolDown(attackCoolDownTime, attackButton, attackImage);
                SwapWeaponCoolDown(attackCoolDownTime);

                comboResetCount++;
            }
            else
            {
                player.ChangeState(PlayerStates.PlayerCharacterIdleState);
            }
        }

        void ShuffleCombo()
        {
            if (GetWeaponState())
            {
                ResetComboCount(shortSwordAttackCount);
            }
            else
            {
                ResetComboCount(longSwordAttackCount);
            }
        }

        void ResetComboCount(int Num)
        {
            int temp = 0;

            if (comboResetCount == Num)
            {
                commandControll.ClearCommand();

                for (int i = 0; i < Num; i++)
                {
                    temp = Random.Range(1, 3);
                    comboRandom = temp;
                }
                comboResetCount = 0;
            }
        }


        IEnumerator ButtonCoolDownCoroutine(float time, Button button)
        {
            if (button == weaponSwapButton)
            {
                isSwap = true;
            }

            button.interactable = false;
            yield return new WaitForSeconds(time);
            button.interactable = true;

            isSwap = false;
        }

        IEnumerator ImageCoolDown(float time, Image image)
        {
            while (image.fillAmount > 0)
            {
                image.fillAmount -= 1 / time * Time.deltaTime;
                yield return null;
            }
            image.fillAmount = 1;
        }

        void StartButtonCoolDown(float time, Button button, Image image)
        {
            StartCoroutine(ButtonCoolDownCoroutine(time, button));
            StartCoroutine(ImageCoolDown(time, image));
        }

        void SwapWeaponCoolDown(float time)
        {
            if (isSwap == false)
            {
                StartButtonCoolDown(time, weaponSwapButton, weaponSwapImage);
            }
        }

        bool GetWeaponState()
        {
            if(player.CurrentWeaponState == PlayerCharacterWeaponState.ShortSword)
            {
                return true;
            }
            else return false;
        }

        public void SetpUpUI()
        {
            // ID.text = AccountInfo.Instance.Id;

            level.text = "Level\n" + player.Level.ToString();

            UpdateHpUI();

            expValue = (player.Exp > player.MaxExp) ? 99.99f : player.Exp / player.MaxExp * standardPercent;
            expBar.fillAmount = player.Exp / player.MaxExp;
            exp.text = expValue.ToString("N1") + "%" ;
        }      
        public void UpdateHpUI()
        {
            hpValue = player.HealthPoint / player.MaxHealthPoint * standardPercent;
            hpBar.fillAmount = player.HealthPoint / player.MaxHealthPoint;
            hp.text = (hpValue < 0) ? 0.0f + "%" : hpValue.ToString("N1") + "%";
        }
    }
}