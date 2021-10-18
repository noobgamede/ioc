using System;
using System.Reflection;
namespace IOC.IoC
{
    public interface IProvider
    {
        bool Create(Type containerContract,PropertyInfo info,out object instance);
        Type Contract { get; }
    }

    public interface IProvider<T> : IProvider { }
}
