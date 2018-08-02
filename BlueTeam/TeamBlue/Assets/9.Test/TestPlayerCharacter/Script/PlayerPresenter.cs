using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField]
    Button AttackButtons, SkillButtons, BackStepButtons, WeaponSwapButtons;

    [SerializeField]
    JoyStick joyStick;

    Player player;

    CommandControll commandControll;

    ICommand Attack1, Attack2, Attack3, Attack4;

    //public delegate void SkillHandler();

    //public static event SkillHandler SkillTouch;


    Vector3 inputMoveVector;

    int comboResetCount;

    int comboRandom;

    bool isComboState;

    float horizontal, vertical;

    Vector3 moverDir;

    void Start()
    {
        inputMoveVector = Vector3.zero;

        comboResetCount = 0;
        comboRandom = Random.Range(1, 2);

        isComboState = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        commandControll = new CommandControll();

        Attack1 = new CommandAttack1();
        Attack2 = new CommandAttack2();

        Attack3 = new CommandAttack3();
        Attack4 = new CommandAttack4();

        AttackButtons.onClick.AddListener(() => RandomCombo());

        AttackButtons.onClick.AddListener(() => StartCombo());

        AttackButtons.onClick.AddListener(() => Shuffle());

        SkillButtons.onClick.AddListener(() => player.Skill1());

        BackStepButtons.onClick.AddListener(() => InputBackStep());

        WeaponSwapButtons.onClick.AddListener(() => WeaponSwapButton());
    }
    void Update()
    {
        inputMoveVector = PoolInput();
    }

    void FixedUpdate()
    {
        if ((player.PlayerState.GetType() != typeof(PlayerCharacterAttackState) && player.PlayerState.GetType() != typeof(PlayerCharacterBackStepState)))
        {
            PlayerMove();
        }

    }

    Vector3 PoolInput()
    {
        horizontal = joyStick.Horizontal();
        vertical = joyStick.Vertical();

        moverDir = new Vector3(horizontal, 0, vertical).normalized;

        return moverDir;
    }

    void InputBackStep()
    {
        if (player.PlayerState.GetType() == typeof(PlayerCharacterAttackState) || player.PlayerState.GetType() == typeof(PlayerCharacterRunState))
        {
            return;
        }
        else 
        {
            player.isRunning = false;
            player.SetState(new PlayerCharacterBackStepState(player));
        }
    }

    void PlayerMove()
    {
        if (inputMoveVector == Vector3.zero)
        {
            player.isRunning = false;
            player.moveVector = Vector3.zero;
            player.SetState(new PlayerCharacterIdleState(player));
        }
        else
        {
            player.isRunning = true;
            player.moveVector = inputMoveVector;
            player.SetState(new PlayerCharacterRunState(player));
        }

    }

    void WeaponSwapButton()
    {
        Debug.Log(player.isSwapAble);
        if (player.CurrentWeaponState == PlayerCharacterWeaponState.ShortSword)
        {
            player.WeaponSwitching(PlayerCharacterWeaponState.LongSword);
        }
        else if(player.CurrentWeaponState == PlayerCharacterWeaponState.LongSword)
        {
            player.WeaponSwitching(PlayerCharacterWeaponState.ShortSword);

            isComboState = false;
            commandControll.ClearCommand();
            comboResetCount = 0;
        }

        //무기 바꾸면 콤보 초기화해야됨.
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
        //무기상태 확인
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
        //무기상태 확인
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
        player.isRunning = false;

        player.SetState(new PlayerCharacterAttackState(player));

        isComboState = true;

        comboResetCount++;
        commandControll.ExcuteCommand();
        //StartCoroutine(RestCombo());
    }

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

    //스킬버튼 쿨타임 제어 코루틴 만들자
}