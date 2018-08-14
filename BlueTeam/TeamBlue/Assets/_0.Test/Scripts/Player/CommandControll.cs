using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectB.Characters.Players
{
    public class CommandControll
    {
        //Invoker

        Queue<ICommand> commandQueue = new Queue<ICommand>();

        public void TakeCommand(ICommand command)
        {
            commandQueue.Enqueue(command);
        }

        public void ExcuteCommand()
        {
            foreach (ICommand command in commandQueue)
            {
                command.Execute(); //명령 발동
                commandQueue.Dequeue();
                break;
            }
        }
        public void ClearCommand()
        {
            commandQueue.Clear();
        }
    }
}

