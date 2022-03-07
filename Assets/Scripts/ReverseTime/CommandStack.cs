using Interfaces;
using UnityEngine;
using ReverseTime;

namespace ReverseTime
{
    public class CommandStack
    {
        //private readonly Stack<ICommand> _commandHistory = new Stack<ICommand>();

        private const int StackLimitAmount = 1000;

        private readonly LimitedStack<ICommand> _commandHistory = new LimitedStack<ICommand>(
            StackLimitAmount
        );

        public void ExecuteCommand(ICommand command)
        {
            _commandHistory.Push(command);
            command.Execute();
        }

        public void UndoLastCommand()
        {
            if (_commandHistory.Count <= 0)
                return;

            _commandHistory.Pop().Undo();
        }

        public void StackCount()
        {
            Debug.Log(_commandHistory.Count + " count");
        }
    }
}
