using System;
namespace IOC.Command
{
    interface ICommandFactory
    {
        TCommand Build<TCommand>() where TCommand : ICommand, new();
    }
}
