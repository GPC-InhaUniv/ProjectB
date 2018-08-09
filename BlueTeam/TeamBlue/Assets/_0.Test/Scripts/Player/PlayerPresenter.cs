using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField]
    Button AttackButtons, SkillButtons, BackStepButtons, WeaponButtons;

    [SerializeField]
    JoyStick joyStick;

    Player player;

    CommandControll commandControll;

    ICommand Attack1, Attack2, Attack3, Attack4;


    Vector3 inputMoveVector;

    int comboResetCount;

    int comboRandom;

    bool isComboState;

    float horizontal, vertical;

    Vector3 moverDirection;

    void Start()
    {
        inputMoveVector = Vector3.zero;

        comboResetCount = 0;
        comboRandom = Random.Range(1, 2);

        isComboState = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        commandControll = new CommandControll();

        Attack1 = new CommandAttack1(player);
        Attack2 = new CommandAttack2(player);

        Attack3 = new CommandAttack3(player);
        Attack4 = new CommandAttack4(player);

        AttackButtons.onClick.AddListener(() => RandomCombo());

        AttackButtons.onClick.AddListener(() => StartCombo());

        AttackButtons.onClick.AddListener(() => Shuffle());

        SkillButtons.onClick.AddListener(() => player.SetState(new PlayerCharacterSkillState(player)));

        BackStepButtons.onClick.AddListener(() => InputBackStep());

        WeaponButtons.onClick.AddListener(() => WeaponButton());
    }


    void Update()
    {
        inputMoveVector = PoolInput();
        if ((player.PlayerState.GetType() == typeof(PlayerCharacterAttackState) || player.PlayerState.GetType() == typeof(PlayerCharacterBackStepState)))
        {
            return;
        }
        else
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
        if (inputMoveVector == Vector3.zero)
        {
            player.SetMoveVector(Vector3.zero);
            player.SetState(new PlayerCharacterIdleState(player));
        }
        else
        {
            player.SetMoveVector(inputMoveVector);
            player.SetState(new PlayerCharacterRunState(player));
        }
    }

    void InputBackStep()
    {
        if (player.PlayerState.GetType() == typeof(PlayerCharacterAttackState) || player.PlayerState.GetType() == typeof(PlayerCharacterRunState))
        {
            return;
        }
        else
        {
            player.SetState(new PlayerCharacterBackStepState(player));
        }
    }


    void WeaponButton()
    {
        if (player.CurrentWeaponState == PlayerCharacterWeaponState.ShortSword)
        {
            player.WeaponSwitching(PlayerCharacterWeaponState.LongSword);
        }
        else if(player.CurrentWeaponState == PlayerCharacterWeaponState.LongSword)
        {
            player.WeaponSwitching(PlayerCharacterWeaponState.ShortSword);
        }

        isComboState = false;
        commandControll.ClearCommand();
        comboResetCount = 0;
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
        if (player.IsRunning != true)
        {
            player.SetState(new PlayerCharacterAttackState(player));
            isComboState = true;

            comboResetCount++;

            commandControll.ExcuteCommand();
            //StartCoroutine(RestCombo());
        }
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