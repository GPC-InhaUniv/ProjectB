using UnityEngine;

public abstract class PlayerCharacterState
{
    protected Player player;

    public abstract void Tick(Vector3 moveVector);

    public PlayerCharacterState(Player player)
    {
        this.player = player;
    }

}

public class PlayerCharacterIdleState : PlayerCharacterState
{
    public PlayerCharacterIdleState(Player player) : base(player)
    {

    }

    public override void Tick(Vector3 moveVector)
    {
        player.CharacterAttackPower = 10;
        player.collider.enabled = true;
        player.IsAttacking = false;
        player.IsRunning = false;
        player.IsBackStepping = false;
    }
}

public class PlayerCharacterRunState : PlayerCharacterState
{
    public PlayerCharacterRunState(Player player) : base(player)
    {
  
    }

    public override void Tick(Vector3 moveVector)
    {
        player.IsRunning = true;

        player.Turn(moveVector);
        player.Running(moveVector);
    }
}
public class PlayerCharacterAttackState : PlayerCharacterState
{
    public PlayerCharacterAttackState(Player player) : base(player)
    {
    
    }

    public override void Tick(Vector3 moveVector)
    {
        player.IsAttacking = true;

        player.PlayerAttack();
    }
}

public class PlayerCharacterSkillState : PlayerCharacterState
{
    public PlayerCharacterSkillState(Player player) : base(player)
    {

    }

    public override void Tick(Vector3 moveVector)
    {
        player.IsAttacking = true;

        player.CharacterAttackPower = 20;


        player.Skill();
    }
}

public class PlayerCharacterBackStepState : PlayerCharacterState
{
    public PlayerCharacterBackStepState(Player player) : base(player)
    {

    }

    public override void Tick(Vector3 moveVector)
    {
        player.IsBackStepping = true;
        player.collider.enabled = false;

        player.BackStep();
    }
}
public class PlayerCharacterDieState : PlayerCharacterState
{
    public PlayerCharacterDieState(Player player) : base(player)
    {

    }

    public override void Tick(Vector3 moveVector)
    {
        player.CharacterAttackPower = 0;
        player.collider.enabled = false;
        player.IsAttacking = false;
        player.IsRunning = false;
        player.IsBackStepping = false;

        player.Die(); 
    }
}


