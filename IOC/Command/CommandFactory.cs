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
            OnNewCommand(command);
            return command;
        }

        public TCommand Build<TCommand,T>(T dependency) where TCommand : ICommand,new()
        {
            Type commandClass = typeof(TCommand);
            TCommand command = (TCommand)Activator.CreateInstance(commandClass,dependency);
            OnNewCommand(command);
            return command;
        }

        void OnNewCommand(ICommand command)
        {
            if (_onNewCommand != null) _onNewCommand(command); 
        }
    }
}
