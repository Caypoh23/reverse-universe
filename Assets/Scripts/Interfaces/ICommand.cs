﻿namespace Interfaces
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}