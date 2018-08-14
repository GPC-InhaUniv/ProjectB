using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Players
{
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
            player.SetAttackNumber((int)AttackNumber.Attack1);
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
            player.SetAttackNumber((int)AttackNumber.Attack2);
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
            player.SetAttackNumber((int)AttackNumber.Attack3);
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
            player.SetAttackNumber((int)AttackNumber.Attack4);
        }
    }
    public enum AttackNumber
    {
        Attack1 = 1,
        Attack2 = 2,
        Attack3 = 3,
        Attack4 = 4
    }
}
