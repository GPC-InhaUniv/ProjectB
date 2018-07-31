using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacterState
{
 
    public PlayerCharacterState(Player player)
    {
        this.player = player;
    }
    protected Player player;

    public abstract void Tick(Vector3 moveVector);


}
public class PlayerCharacterRunState : PlayerCharacterState
{
    public PlayerCharacterRunState(Player player) : base(player) { }

    public override void Tick(Vector3 moveVector)
    {
        player.TurnToPC(moveVector);
        player.RunToPC(moveVector);
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
        player.BackStepToPC();
    }
}

