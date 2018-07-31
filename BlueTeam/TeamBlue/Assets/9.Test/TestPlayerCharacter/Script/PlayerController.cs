using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Player player;

    CommandControll commandControll;

    ICommand Attack1, Attack2, Attack3, Attack4;

    //public delegate void SkillHandler();

    //public static event SkillHandler SkillTouch;

    [SerializeField]
    Button AttackButtons, SkillButtons, BackStepButtons, WeaponSwapButtons;

    [SerializeField]
    IOnclick Virtualonclick;
 
    Vector3 moveVector;

    int comboResetCount;

    int comboRandom;

    bool isComboState;
    float h, v;

    Vector3 moverDir;

    void Start()
    {
        moveVector = Vector3.zero;

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

        //스킬사용할 버튼 SkillButtons.onClick.AddListener(() => );

        BackStepButtons.onClick.AddListener(() => InputBackStep());

        WeaponSwapButtons.onClick.AddListener(() => WeaponSwapButton());
    }
    void Update()
    {
        moveVector = PoolInput();
    }

    void FixedUpdate()
    {
        if ((player.PcState.GetType() != typeof(PlayerCharacterAttackState) && player.PcState.GetType() != typeof(PlayerCharacterBackStepState)))
        {
            PlayerMove();
        }

    }

    Vector3 PoolInput()
    {
        h = Virtualonclick.Horizontal();
        v = Virtualonclick.Vertical();

        moverDir = new Vector3(h, 0, v).normalized;

        return moverDir;
    }

    void InputBackStep()
    {
        if(player.PcState.GetType() == typeof(PlayerCharacterAttackState))
        {
            return;
        }
        else if(player.PcState.GetType() != typeof(PlayerCharacterRunState))
        {
            Debug.Log("백스텝 호출");
            player.isRunning = false;
            player.SetState(new PlayerCharacterBackStepState(player));
        }
    }

    void PlayerMove()
    {
        if (moveVector == Vector3.zero)
        {
            player.isRunning = false;
            player.moveVector = Vector3.zero;
            player.SetState(new PlayerCharacterIdleState(player));
        }
        else
        {
            player.isRunning = true;
            player.moveVector = moveVector;
            player.SetState(new PlayerCharacterRunState(player));
        }
     
    }

    void WeaponSwapButton()
    {
        if (player.CurrentweaponState == PlayerCharacterWeaponState.ShortSword)
        {
            player.WeaponSwitching(PlayerCharacterWeaponState.LongSword);
        }
        else
            player.WeaponSwitching(PlayerCharacterWeaponState.ShortSword);

        isComboState = false;
        commandControll.ClearCommand();
        comboResetCount = 0;
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
        if (player.CurrentweaponState == PlayerCharacterWeaponState.ShortSword)
        {
            if (comboResetCount == 0)
            {
                commandControll.TakeCommand(Attack1);
                commandControll.TakeCommand(Attack2);
                commandControll.TakeCommand(Attack3);
            }
        }
        else if (player.CurrentweaponState == PlayerCharacterWeaponState.LongSword)
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
        if(player.CurrentweaponState == PlayerCharacterWeaponState.ShortSword)
        {
            if (comboResetCount == 0)
            {
                commandControll.TakeCommand(Attack1);
                commandControll.TakeCommand(Attack2);
                commandControll.TakeCommand(Attack4);
            }
        }
        else if (player.CurrentweaponState == PlayerCharacterWeaponState.LongSword)
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
}