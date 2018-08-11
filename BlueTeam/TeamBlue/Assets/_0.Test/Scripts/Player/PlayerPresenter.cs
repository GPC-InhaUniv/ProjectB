using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField]
    Button AttackButton, SkillButton, BackStepButton, WeaponSwapButton;

    [SerializeField]
    Image SkillImage, BackStepImage, WeaponSwapImage;
    [SerializeField]
    JoyStick joyStick;

    Player player;

    CommandControll commandControll;

    ICommand Attack1, Attack2, Attack3, Attack4;


    Vector3 inputMoveVector;

    int comboResetCount;

    int comboRandom;

    float skillCoolDownTime, backStepCoolDownTime, swapCoolDownTime;

    bool isComboState;

    float horizontal, vertical;

    Vector3 moverDirection;

    bool isSwap = false;

    Coroutine coroutine;
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
        isComboState = false;
        GetImage();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        commandControll = new CommandControll();

        Attack1 = new CommandAttack1(player);
        Attack2 = new CommandAttack2(player);

        Attack3 = new CommandAttack3(player);
        Attack4 = new CommandAttack4(player);


        AttackButton.onClick.AddListener(() => RandomCombo());
        AttackButton.onClick.AddListener(() => StartCombo());
        AttackButton.onClick.AddListener(() => Shuffle());
        //AttackButton.onClick.AddListener(() => ButtonCoolDown(ButtonName.WeaponSwapButton));

        SkillButton.onClick.AddListener(() => InputSkillButton());
        //SkillButton.onClick.AddListener(() => ButtonCoolDown(ButtonName.SkillButton));

        BackStepButton.onClick.AddListener(() => InputBackStep());
        //BackStepButton.onClick.AddListener(() => ButtonCoolDown(ButtonName.BackStepButton));

        WeaponSwapButton.onClick.AddListener(() => InputWeaponSwapButton());
        //WeaponSwapButton.onClick.AddListener(() => ButtonCoolDown(ButtonName.WeaponSwapButton));
    }
    void Update()
    {
        inputMoveVector = PoolInput();
        if (PlayerState())
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
    
    void ButtonCoolDown(ButtonName name)
    {
        switch (name)
        {
            case ButtonName.SkillButton:

                break;
            case ButtonName.BackStepButton:

                break;
            case ButtonName.WeaponSwapButton:


                break;
            default:
                break;
        }
    }

    IEnumerator ButtonCoolDownCoroutine(float time, Button name)
    {
        isSwap = true;
        name.interactable = false;
        yield return new WaitForSeconds(time);
        name.interactable = true;
        isSwap = false;
    }

    IEnumerator ImageCoolDown(float time, Image name)
    {

        while(name.fillAmount > 0)
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
            player.SetState(new PlayerCharacterRunState(player));
        }
        else
        {
            player.SetState(new PlayerCharacterIdleState(player));
        }
    }

    void InputBackStep()
    {
        if (PlayerIdleState())
        {
            player.SetState(new PlayerCharacterBackStepState(player));
            StartCoroutine(ButtonCoolDownCoroutine(backStepCoolDownTime, BackStepButton));
            StartCoroutine(ImageCoolDown(backStepCoolDownTime, BackStepImage));
        }
        else
        {
            player.SetState(new PlayerCharacterIdleState(player));
        }

    }

    bool PlayerState()
    {
        if (player.PlayerState.GetType() != typeof(PlayerCharacterAttackState)  && player.PlayerState.GetType() != typeof(PlayerCharacterBackStepState) && player.PlayerState.GetType() != typeof(PlayerCharacterSkillState))
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
        //수정필요함
        if (PlayerIdleState())
        {
            player.SetState(new PlayerCharacterSkillState(player));

            StartCoroutine(ButtonCoolDownCoroutine(skillCoolDownTime, SkillButton));
            StartCoroutine(ImageCoolDown(skillCoolDownTime, SkillImage));
        }
        else
        {
            player.SetState(new PlayerCharacterIdleState(player));
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
            if (isSwap == false)
            {
                StartCoroutine(ButtonCoolDownCoroutine(swapCoolDownTime, WeaponSwapButton));
                StartCoroutine(ImageCoolDown(swapCoolDownTime, WeaponSwapImage));
            }
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
            player.SetState(new PlayerCharacterAttackState(player));

            isComboState = true;

            comboResetCount++;

            commandControll.ExcuteCommand();
            //StartCoroutine(RestCombo());
            if (isSwap == false)
            {
                StartCoroutine(ButtonCoolDownCoroutine(swapCoolDownTime, WeaponSwapButton));
                StartCoroutine(ImageCoolDown(swapCoolDownTime, WeaponSwapImage));
            }
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

    IEnumerator WaitForSecondCommand(float second)
    {
        yield return new WaitForSeconds(second);
    }
    //수정 필요함

    enum ButtonName
    {
        AttackButton,
        SkillButton,
        BackStepButton,
        WeaponSwapButton
    }
}