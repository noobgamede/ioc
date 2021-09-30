using System;
namespace IOC.Command
{
    internal interface IInjectableCommand:ICommand
    {
        ICommand Inject<T>(T dependency);
    }

    internal interface IMultiInjectableCommand:ICommand
    {
        ICommand Inject(params object[] notifications); 
    }
}
