using System;
using System.Collections.Generic;
namespace IOC.Command
{
    sealed public class CommandFactory : ICommandFactory
    {
        Action<ICommand> _onNewCommand;
        public CommandFactory()
        { 
        }

        public CommandFactory(Action<ICommand> onNewCommand)
        {
            _onNewCommand = onNewCommand;
        }

        public TCommand Build<TCommand>() where TCommand : ICommand, new()
        {
            TCommand command = new TCommand();
            _onNewCommand?.Invoke(command);
            return command;
        }

        public TCommand Build<TCommand,T>(T dependency) where TCommand : ICommand,new()
        {
            Type commandClass = typeof(TCommand);
            TCommand command = (TCommand)Activator.CreateInstance(commandClass,dependency);
            _onNewCommand?.Invoke(command);
            return command;
        }


    }
}
