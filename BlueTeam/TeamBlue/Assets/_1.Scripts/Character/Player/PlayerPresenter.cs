using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.Characters.Players;

namespace ProjectB.UI.Presenter
{
    public class PlayerPresenter : MonoBehaviour
    {
        [SerializeField]
        Button attackButton, skillButton, backStepButton, weaponSwapButton;
        [SerializeField]
        Image skillImage, backStepImage, weaponSwapImage, attackImage;

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

        float skillCoolDownTime, backStepCoolDownTime, swapCoolDownTime, attackCoolDownTime;

        float horizontal, vertical;

        float expValue, hpValue;

        bool isComboState;

        bool isSwap;

        Vector3 moverDirection;  

     
        const int shortSwordAttackCount = 3;
        const int longSwordAttackCount = 2;

        void Start()
        {

            Initialization(); // 삭제예정

            commandControll = new CommandControll();

            Attack1 = new CommandAttack1(player);
            Attack2 = new CommandAttack2(player);
            Attack3 = new CommandAttack3(player);
            Attack4 = new CommandAttack4(player);


            skillCoolDownTime = 5.0f;
            backStepCoolDownTime = 3.0f;
            swapCoolDownTime = 2.0f;
            attackCoolDownTime = 1.2f;

            inputMoveVector = Vector3.zero;
            
            comboResetCount = 0;
            comboRandom = Random.Range(1, 3);

            isSwap = false;
            isComboState = false;

            GetImage();

            attackButton.onClick.AddListener(() => RandomCombo());
            attackButton.onClick.AddListener(() => StartCombo());
            attackButton.onClick.AddListener(() => Shuffle());

            skillButton.onClick.AddListener(() => InputSkillButton());

            backStepButton.onClick.AddListener(() => InputBackStep());

            weaponSwapButton.onClick.AddListener(() => InputWeaponSwapButton());
        }

        void Initialization()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        void Update()
        {
            inputMoveVector = PoolInput();

            if (GetReadyForRun())
            {
                SetInputVector();
            }
        }

        void GetImage()
        {
            skillImage = skillButton.GetComponent<Image>();
            backStepImage = backStepButton.GetComponent<Image>();
            weaponSwapImage = weaponSwapButton.GetComponent<Image>();
            attackImage = attackButton.GetComponent<Image>();
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

            if (button == weaponSwapButton)
            {
                isSwap = false;
            }
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
            if (GetPlayerIdleState())
            {
                player.ChangeState(PlayerStates.PlayerCharacterBackStepState);

                StartButtonCoolDown(backStepCoolDownTime, backStepButton, backStepImage);

                SwapWeaponCoolDown();
            }
            else
            {
                player.ChangeState(PlayerStates.PlayerCharacterIdleState);
            }
        }

        bool GetReadyForRun()
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

        bool GetPlayerIdleState()
        {
            if (player.PlayerState.GetType() == typeof(PlayerCharacterIdleState))
            {
                return true;
            }
            else return false;
        }

        void InputSkillButton()
        {
            if (GetPlayerIdleState())
            {
                player.ChangeState(PlayerStates.PlayerCharacterSkillState);

                StartButtonCoolDown(skillCoolDownTime, skillButton, skillImage);

                SwapWeaponCoolDown();
            }
            else
            {
                player.ChangeState(PlayerStates.PlayerCharacterIdleState);
            }
        }

        void InputWeaponSwapButton()
        {
            if (GetPlayerIdleState())
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
            if (GetPlayerIdleState())
            {
                commandControll.ExcuteCommand();
                player.ChangeState(PlayerStates.PlayerCharacterAttackState);
                StartButtonCoolDown(attackCoolDownTime, attackButton, attackImage);
                SwapWeaponCoolDown();

                isComboState = true;

                comboResetCount++;
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
                StartButtonCoolDown(swapCoolDownTime, weaponSwapButton, weaponSwapImage);
            }
        }

        void Shuffle()
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

        bool GetWeaponState()
        {
            if(player.CurrentWeaponState == PlayerCharacterWeaponState.ShortSword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateUI()
        {
            levelText.text = "Level\n" + player.CharacterLevel.ToString();

            expValue = player.CharacterExp / player.PlayerMaxExp * 100;
            hpValue = player.CharacterHealthPoint / player.CharacterMaxHealthPoint * 100;

            hpBar.fillAmount = player.CharacterHealthPoint / player.CharacterMaxHealthPoint;
            hpValueText.text = (100.0f > hpValue) ? hpValue + "%" : "100%" ;

   
            expBar.fillAmount = player.CharacterExp / player.PlayerMaxExp;
            expValueText.text = (100.0f > expValue) ? expValue.ToString("N1") + "%"  : "100%";
        }

    }
}