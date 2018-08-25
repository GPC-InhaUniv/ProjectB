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
        PlayerAnimation playerAinmaton;
        public CommandAttack1(PlayerAnimation playerAinmaton)
        {
            this.playerAinmaton = playerAinmaton;
        }
        public void Execute()
        {
            playerAinmaton.AttackAnimation(AttackNumber.Attack1.ToString());
        }
    }
    public class CommandAttack2 : ICommand
    {
        PlayerAnimation playerAinmaton;
        public CommandAttack2(PlayerAnimation playerAinmaton)
        {
            this.playerAinmaton = playerAinmaton;
        }
        public void Execute()
        {
            playerAinmaton.AttackAnimation(AttackNumber.Attack2.ToString());
        }
    }
    public class CommandAttack3 : ICommand
    {
        PlayerAnimation playerAinmaton;
        public CommandAttack3(PlayerAnimation playerAinmaton)
        {
            this.playerAinmaton = playerAinmaton;
        }
        public void Execute()
        {
            playerAinmaton.AttackAnimation(AttackNumber.Attack3.ToString());
        }
    }
    public class CommandAttack4 : ICommand
    {
        PlayerAnimation playerAinmaton;
        public CommandAttack4(PlayerAnimation playerAinmaton)
        {
            this.playerAinmaton = playerAinmaton;
        }
        public void Execute()
        {
            playerAinmaton.AttackAnimation(AttackNumber.Attack4.ToString());
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
