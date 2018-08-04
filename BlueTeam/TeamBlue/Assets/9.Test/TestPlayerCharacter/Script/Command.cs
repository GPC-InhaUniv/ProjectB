using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
}

public class CommandAttack1 : ICommand
{
    Player player = null;
    public CommandAttack1(Player player)
    {
        this.player = player;
    }
    public void Execute()
    {
        player.attackNum = 1;
        player.PlayerAttack();
    }
}
public class CommandAttack2 : ICommand
{
    Player player = null;
    public CommandAttack2(Player player)
    {
        this.player = player;
    }
    public void Execute()
    {
        player.attackNum = 2;
        player.PlayerAttack();
    }
}
public class CommandAttack3 : ICommand
{
    Player player = null;
    public CommandAttack3(Player player)
    {
        this.player = player;
    }
    public void Execute()
    {
        player.attackNum = 3;
        player.PlayerAttack();
    }
}
public class CommandAttack4 : ICommand
{
    Player player = null;
    
    public CommandAttack4(Player player)
    {
        this.player = player;
    }
    public void Execute()
    {
        player.attackNum = 4;
        player.PlayerAttack();
    }
}
