using System;
namespace IOC.Command
{
    public interface IInjectableCommand<T>:ICommand
    {
        ICommand Inject(T dependency);
    }

    public interface IInjectableCommandWithStruct<T> : ICommand where T:struct 
    {
        ICommand Inject(ref T dependency); 
    }
}
