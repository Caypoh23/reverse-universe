using System.Collections.Generic;
using Interfaces;
using ReverseTime.Commands;
using TMPro;

namespace ReverseTime
{
    public class CommandStack
    {
        private readonly Stack<ICommand> _commandHistory = new Stack<ICommand>();

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

        public void ClearCache()
        {
            _commandHistory.Clear();
        }
    }
}