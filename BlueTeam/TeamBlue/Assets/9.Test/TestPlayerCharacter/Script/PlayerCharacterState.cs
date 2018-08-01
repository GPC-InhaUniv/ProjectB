using System.Collections;
using System.Collections.Generic;
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
public class PlayerCharacterRunState : PlayerCharacterState
{
    public PlayerCharacterRunState(Player player) : base(player) { }

    public override void Tick(Vector3 moveVector)
    {
        player.Turn(moveVector);
        player.Running(moveVector);
    }
}
public class PlayerCharacterAttackState : PlayerCharacterState
{
    public PlayerCharacterAttackState(Player player) : base(player) { }

    public override void Tick(Vector3 moveVector)
    {
        player.PlayerAttack();
    }
}
public class PlayerCharacterIdleState : PlayerCharacterState
{
    public PlayerCharacterIdleState(Player player) : base(player) { }

    public override void Tick(Vector3 moveVector)
    {
        return;
    }
}
public class PlayerCharacterBackStepState : PlayerCharacterState
{
    public PlayerCharacterBackStepState(Player player) : base(player) { }

    public override void Tick(Vector3 moveVector)
    {
        player.BackStep();
    }
}

