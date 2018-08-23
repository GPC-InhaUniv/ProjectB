using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectB.Characters.Players;
using ProjectB.GameManager;
namespace ProjectB.UI.Presenter
{
    public class PlayerPresenter : MonoBehaviour , IInitializable
    {
        [SerializeField]
        Button attackButton, skillButton, backStepButton, weaponSwapButton;
        [SerializeField]
        Image skillImage, backStepImage, weaponSwapImage, attackImage;

        [SerializeField]
        Image hpBar,expBar;
        [SerializeField]
        Text levelText,hpValueText,expValueText, playerId;

        [SerializeField]
        JoyStick joyStick;

        Player player;

        CommandControll commandControll;

        ICommand Attack1, Attack2, Attack3, Attack4;

        Vector3 inputMoveVector;

        Vector3 moverDirection;

        float skillCoolDownTime, backStepCoolDownTime, swapCoolDownTime, attackCoolDownTime;

        float horizontal, vertical;

        float expValue, hpValue;

        bool isComboState, isSwap;

        int comboResetCount;

        int comboRandom;

        int standardPercent = 100;
     
        const int shortSwordAttackCount = 3;
        const int longSwordAttackCount = 2;

        void Start()
        {
            //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            //Attack1 = new CommandAttack1(player);
            //Attack2 = new CommandAttack2(player);
            //Attack3 = new CommandAttack3(player);
            //Attack4 = new CommandAttack4(player);
            //상단 5줄은 테스트용임, 오류날 시 주석처리 

            skillCoolDownTime = 5.0f;
            backStepCoolDownTime = 3.5f;
            swapCoolDownTime = 2.5f;
            attackCoolDownTime = 1.0f;

            inputMoveVector = Vector3.zero;
            
            comboResetCount = 0;
            comboRandom = Random.Range(1, 3);

            isSwap = false;
            isComboState = false;

            GetImage();
            commandControll = new CommandControll();

            attackButton.onClick.AddListener(() => RandomCombo());
            attackButton.onClick.AddListener(() => StartCombo());
            attackButton.onClick.AddListener(() => Shuffle());

            skillButton.onClick.AddListener(() => InputSkillButton());

            backStepButton.onClick.AddListener(() => InputBackStep());

            weaponSwapButton.onClick.AddListener(() => InputWeaponSwapButton());
        }
       
        public void Initialize()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Attack1 = new CommandAttack1(player);
            Attack2 = new CommandAttack2(player);
            Attack3 = new CommandAttack3(player);
            Attack4 = new CommandAttack4(player);
        }

        void Update()
        {
            if (player == null)
                return;
            inputMoveVector = PoolInput();

            if (GetIsRunningState(player.IsWorking))
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

        void SwapWeaponCoolDown()
        {
            if (isSwap == false)
            {
                StartButtonCoolDown(swapCoolDownTime, weaponSwapButton, weaponSwapImage);
            }

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
                player.ChangeState(PlayerStates.PlayerCharacterRunState);
            }
            else
            {
                player.ChangeState(PlayerStates.PlayerCharacterIdleState);
            }
        }

        void InputBackStep()
        {
            if (GetIsRunningState(player.IsRunning))
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



        void InputSkillButton()
        {
            if (GetIsRunningState(player.IsRunning))
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
            if (GetIsRunningState(player.IsRunning))
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
            if (GetIsRunningState(player.IsRunning))
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


        bool GetIsRunningState(bool state)
        {
            if (state)
            {
                return false;
            }
            else return true;
        }

        public void UpdateUI()
        {
            //playerId.text = AccountInfo.Instance.Id;
            levelText.text = "Level\n" + player.CharacterLevel.ToString();

            expValue = player.CharacterExp / player.PlayerMaxExp * standardPercent;
            hpValue = player.CharacterHealthPoint / player.CharacterMaxHealthPoint * standardPercent;

            hpBar.fillAmount = player.CharacterHealthPoint / player.CharacterMaxHealthPoint;
            hpValueText.text = (hpValue > 0) ? hpValue.ToString("N1") + "%" : 0 + "%";
   
            expBar.fillAmount = player.CharacterExp / player.PlayerMaxExp;
            expValueText.text = (expValue > 0) ? expValue.ToString("N1") + "%"  :0 + "%";
        }      
    }
}