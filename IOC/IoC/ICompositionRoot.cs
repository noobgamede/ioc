using System;
namespace IOC.IoC
{
    public interface ICompositionRoot
    {
        IContainer container { get; }
    }
}
