using System;
namespace IOC.Command
{
    internal interface ICommandFactory
    {
        TCommand Build<TCommand>() where TCommand : ICommand, new();
    }
}
