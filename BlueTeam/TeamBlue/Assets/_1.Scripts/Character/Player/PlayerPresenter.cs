using System.Collections;
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

        CommandControll commandControll;

        ICommand Attack1, Attack2, Attack3, Attack4;

        Vector3 inputMoveVector, moverDirection;

        float skillCoolDownTime, backStepCoolDownTime, swapCoolDownTime, attackCoolDownTime;

        float horizontal, vertical, expValue, hpValue;

        bool isSwap;

        int comboResetCount, comboRandom;

        const int standardPercent = 100;
     
        const int shortSwordAttackCount = 3;
        const int longSwordAttackCount = 2;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Attack1 = new CommandAttack1(player.PlayerAinmaton);
            Attack2 = new CommandAttack2(player.PlayerAinmaton);
            Attack3 = new CommandAttack3(player.PlayerAinmaton);
            Attack4 = new CommandAttack4(player.PlayerAinmaton);
            //상단 5줄은 테스트용임, 오류날 시 주석처리

            skillCoolDownTime = 4.0f;
            backStepCoolDownTime = 2.0f;
            swapCoolDownTime = 2.0f;
            attackCoolDownTime = 1.0f;

            inputMoveVector = Vector3.zero;
            
            comboResetCount = 0;
            comboRandom = Random.Range(1, 3);

            isSwap = false;

            GetImage();
            commandControll = new CommandControll();

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
            Attack1 = new CommandAttack1(player.PlayerAinmaton);
            Attack2 = new CommandAttack2(player.PlayerAinmaton);
            Attack3 = new CommandAttack3(player.PlayerAinmaton);
            Attack4 = new CommandAttack4(player.PlayerAinmaton);
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
            if (player == null)
                return;

            inputMoveVector = PoolInput();

            if (!GetIsState(player.IsWorking))
            {
                SetInputVector();
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
            if (GetIsState(player.IsWorking)) return;

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
            if (GetIsState(player.IsWorking)) return;

            if (!GetIsState(player.IsRunning) && player.TargetVector != Vector3.zero)
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
            if (GetIsState(player.IsWorking)) return;

            if (!GetIsState(player.IsRunning))
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
            if (GetIsState(player.IsWorking)) return;

            if (!GetIsState(player.IsRunning))
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
                commandControll.TakeCommand(Attack1);
                commandControll.TakeCommand(Attack2);
                commandControll.TakeCommand(Attack3);
            }
            else
            {
                commandControll.TakeCommand(Attack1);
                commandControll.TakeCommand(Attack2);
            }
        }

        void ComboTwo()
        {
            if (comboResetCount > 0) return;
            if (GetWeaponState())
            {
                commandControll.TakeCommand(Attack1);
                commandControll.TakeCommand(Attack2);
                commandControll.TakeCommand(Attack4);
            }
            else
            {
                commandControll.TakeCommand(Attack1);
                commandControll.TakeCommand(Attack3);
            }

        }

        void StartCombo()
        {
            if (GetIsState(player.IsWorking)) return;

            if (!GetIsState(player.IsRunning))
            {
                player.ChangeState(PlayerStates.PlayerCharacterAttackState);
                commandControll.ExcuteCommand();

                StartButtonCoolDown(attackCoolDownTime, attackButton, attackImage);
                SwapWeaponCoolDown();

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

        void SwapWeaponCoolDown()
        {
            if (isSwap == false)
            {
                StartButtonCoolDown(swapCoolDownTime, weaponSwapButton, weaponSwapImage);
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

        bool GetIsState(bool state)
        {
            if (state)
            {
                return true;
            }
            else return false;
        }

        public void SetpUpUI()
        {
            //ID.text = AccountInfo.Instance.Id;
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
            hp.text = hpValue.ToString("N1") + "%";
        }
    }
}