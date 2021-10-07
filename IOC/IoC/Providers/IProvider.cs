using System;

namespace IOC.IoC
{
    public interface IProvider
    {
        object Create(Type containerContract);
        Type Contract { get; }
        bool Single { get; }
    }

    public interface IProvider<T> : IProvider { }
}
