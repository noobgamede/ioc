using System;
namespace IOC.Command
{
    internal interface ICommandFactory
    {
        TCommand Build<TCommand>() where TCommand : ICommand, new();
        TCommand Build<TCommand>(params object[] args) where TCommand : ICommand;
    }
}
