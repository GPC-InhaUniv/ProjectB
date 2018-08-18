﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.Characters.Players;

namespace ProjectB.UI.Presenter
{
    enum ButtonName
    {
        AttackButton,
        SkillButton,
        BackStepButton,
        WeaponSwapButton
    }

    public class PlayerPresenter : MonoBehaviour
    {
        [SerializeField]
        Button AttackButton, SkillButton, BackStepButton, WeaponSwapButton;
        [SerializeField]
        Image SkillImage, BackStepImage, WeaponSwapImage;

        [SerializeField]
        Image hpBar,expBar;
        [SerializeField]
        Text levelText,hpValueText,expValueText;

        [SerializeField]
        JoyStick joyStick;

        Player player;

        CommandControll commandControll;

        ICommand Attack1, Attack2, Attack3, Attack4;


        Vector3 inputMoveVector;

        int comboResetCount;

        int comboRandom;

        float skillCoolDownTime, backStepCoolDownTime, swapCoolDownTime;

        float horizontal, vertical;

        float expValue;

        bool isComboState;

        bool isSwap;

        Vector3 moverDirection;



        //minimap
        MinimapRadar minimap;

        [SerializeField]
        public List<RectTransform> enemyIconPositions;

        const float minimapScale = 5.0f;

        Vector2 playerPosition;
        Vector2 enemyPosition;

        public RectTransform IconsParent;

        [SerializeField]
        RectTransform enemyIcon;
        //minimap


        void Start()
        {
            skillCoolDownTime = 5.0f;
            backStepCoolDownTime = 3.0f;
            swapCoolDownTime = 2.0f;

            inputMoveVector = Vector3.zero;
            
            //수정 필요
            comboResetCount = 0;
            comboRandom = Random.Range(1, 2);
            //수정 필요
            isSwap = false;
            isComboState = false;

            GetImage();

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            minimap = player.GetComponentInChildren<MinimapRadar>();

            commandControll = new CommandControll();

            Attack1 = new CommandAttack1(player);
            Attack2 = new CommandAttack2(player);

            Attack3 = new CommandAttack3(player);
            Attack4 = new CommandAttack4(player);


            AttackButton.onClick.AddListener(() => RandomCombo());
            AttackButton.onClick.AddListener(() => StartCombo());

            AttackButton.onClick.AddListener(() => Shuffle());

            SkillButton.onClick.AddListener(() => InputSkillButton());

            BackStepButton.onClick.AddListener(() => InputBackStep());


            WeaponSwapButton.onClick.AddListener(() => InputWeaponSwapButton());

            //minimap
            RegistIcons();//수정 필요
            //minimap
        }


        void Update()
        {
            //minimap     
            //DrawIcons();
            //minimap
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowHUD();
            }


            inputMoveVector = PoolInput();

            if (PlayerNormalState())
            {
                SetInputVector();
            }
        }

        void GetImage()
        {
            SkillImage = SkillButton.GetComponent<Image>();
            BackStepImage = BackStepButton.GetComponent<Image>();
            WeaponSwapImage = WeaponSwapButton.GetComponent<Image>();
        }

        //minimap
        void RegistIcons()
        {
            for (int i = 0; i < 20; i++)
            {
                enemyIconPositions[i] = Instantiate(enemyIcon, IconsParent.rect.position, Quaternion.identity);
                enemyIconPositions[i].transform.parent = enemyIcon.parent;
            }
           
        }

        void DrawIcons()
        {
            playerPosition = new Vector2(player.transform.position.x, player.transform.position.z);

            for (int i = 0; i < minimap.Enemys.Count; i++)
            {
                enemyPosition = new Vector2(minimap.Enemys[i].transform.position.x, minimap.Enemys[i].transform.position.z);
                Vector2 playerToEnemy = enemyPosition - playerPosition;
                enemyIconPositions[i].localPosition = playerToEnemy * minimapScale;
            }

            if (minimap.Enemys.Count < enemyIconPositions.Count)
            {
                for (int i = minimap.Enemys.Count; i < enemyIconPositions.Count; i++)
                {
                    enemyIconPositions[i].localPosition = new Vector3(100f, 0, 0);
                }
            }
        }
        //minimap

        IEnumerator ButtonCoolDownCoroutine(float time, Button button)
        {
            if (button == WeaponSwapButton)
            {
                isSwap = true;
            }

            button.interactable = false;
            yield return new WaitForSeconds(time);
            button.interactable = true;

            if (button == WeaponSwapButton)
            {
                isSwap = false;
            }
        }

        IEnumerator ImageCoolDown(float time, Image name)
        {
            while (name.fillAmount > 0)
            {
                name.fillAmount -= 1 / time * Time.deltaTime;
                yield return null;
            }
            name.fillAmount = 1;
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
            player.MoveVector = inputMoveVector;

            if (inputMoveVector != Vector3.zero)
            {
                player.IsRunning = true;
                player.ChangeState(PlayerStates.PlayerCharacterRunState);
            }
            else
            {
                player.IsRunning = false;
                player.ChangeState(PlayerStates.PlayerCharacterIdleState);
            }
        }

        void InputBackStep()
        {
            if (PlayerIdleState())
            {
                player.ChangeState(PlayerStates.PlayerCharacterBackStepState);
                StartCoroutine(ButtonCoolDownCoroutine(backStepCoolDownTime, BackStepButton));
                StartCoroutine(ImageCoolDown(backStepCoolDownTime, BackStepImage));

                SwapWeaponCoolDown();
            }
            else
            {
                player.ChangeState(PlayerStates.PlayerCharacterIdleState);
            }

        }

        bool PlayerNormalState()
        {
            if (player.PlayerState.GetType() != typeof(PlayerCharacterAttackState) && player.PlayerState.GetType() != typeof(PlayerCharacterBackStepState) && player.PlayerState.GetType() != typeof(PlayerCharacterSkillState))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool PlayerIdleState()
        {
            if (player.PlayerState.GetType() == typeof(PlayerCharacterIdleState))
            {
                return true;
            }
            else return false;
        }

        void InputSkillButton()
        {
            if (PlayerIdleState())
            {
                player.ChangeState(PlayerStates.PlayerCharacterSkillState);

                StartCoroutine(ButtonCoolDownCoroutine(skillCoolDownTime, SkillButton));
                StartCoroutine(ImageCoolDown(skillCoolDownTime, SkillImage));

                SwapWeaponCoolDown();
            }
            else
            {
                player.ChangeState(PlayerStates.PlayerCharacterIdleState);
            }
        }

        void InputWeaponSwapButton()
        {
            if (PlayerIdleState())
            {
                if (player.CurrentWeaponState == PlayerCharacterWeaponState.ShortSword)
                {
                    player.WeaponSwitching(PlayerCharacterWeaponState.LongSword);
                }
                else if (player.CurrentWeaponState == PlayerCharacterWeaponState.LongSword)
                {
                    player.WeaponSwitching(PlayerCharacterWeaponState.ShortSword);
                }

                SwapWeaponCoolDown();

                isComboState = false;
                commandControll.ClearCommand();
                comboResetCount = 0;
            }
        }


        void RandomCombo()
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
            if (player.CurrentWeaponState == PlayerCharacterWeaponState.ShortSword)
            {
                if (comboResetCount == 0)
                {
                    commandControll.TakeCommand(Attack1);
                    commandControll.TakeCommand(Attack2);
                    commandControll.TakeCommand(Attack3);
                }
            }
            else if (player.CurrentWeaponState == PlayerCharacterWeaponState.LongSword)
            {
                if (comboResetCount == 0)
                {
                    commandControll.TakeCommand(Attack1);
                    commandControll.TakeCommand(Attack3);
                }
            }
        }

        void ComboTwo()
        {
            if (player.CurrentWeaponState == PlayerCharacterWeaponState.ShortSword)
            {
                if (comboResetCount == 0)
                {
                    commandControll.TakeCommand(Attack1);
                    commandControll.TakeCommand(Attack2);
                    commandControll.TakeCommand(Attack4);
                }
            }
            else if (player.CurrentWeaponState == PlayerCharacterWeaponState.LongSword)
            {
                if (comboResetCount == 0)
                {
                    commandControll.TakeCommand(Attack1);
                    commandControll.TakeCommand(Attack2);
                }
            }

        }

        void StartCombo()
        {
            if (PlayerIdleState())
            {
                commandControll.ExcuteCommand();
                player.ChangeState(PlayerStates.PlayerCharacterAttackState);

                isComboState = true;

                comboResetCount++;

                //StartCoroutine(RestCombo());

                SwapWeaponCoolDown();
            }
            else
            {
                player.ChangeState(PlayerStates.PlayerCharacterIdleState);
            }
        }

        void SwapWeaponCoolDown()
        {
            if (isSwap == false)
            {
                Debug.Log(isSwap.ToString());
                StartCoroutine(ButtonCoolDownCoroutine(swapCoolDownTime, WeaponSwapButton));
                StartCoroutine(ImageCoolDown(swapCoolDownTime, WeaponSwapImage));
            }
        }



        //수정 필요함
        IEnumerator RestCombo()
        {
            yield return new WaitForSeconds(5.0f);
            if (0 < comboResetCount && isComboState == true)
            {
                Debug.Log("리셋함");

                isComboState = false;
                commandControll.ClearCommand();
                comboResetCount = 0;
            }
        }
        void Shuffle()
        {
            int temp = 0;
            //무기 상태 확인
            if (comboResetCount == 3)
            {
                commandControll.ClearCommand();
                for (int i = 0; i < 3; i++)
                {
                    temp = Random.Range(0, 2) + 1;
                    comboRandom = temp;
                }
                comboResetCount = 0;
            }
            else return;
        }

        public void ShowHUD()
        {
            expValue = player.CharacterExp / player.PlayerMaxExp * 100;

            hpBar.fillAmount = player.CharacterHealthPoint / player.CharacterMaxHealthPoint;
            hpValueText.text = player.CharacterHealthPoint / player.CharacterMaxHealthPoint * 100 + "%";

            levelText.text = "Level\n" + player.CharacterLevel.ToString();       

            expBar.fillAmount = player.CharacterExp / player.PlayerMaxExp;

            expValueText.text = expValue.ToString("N1") + "%";
        }


    }
}