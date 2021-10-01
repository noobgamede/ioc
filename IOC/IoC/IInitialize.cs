using System;
namespace IOC.IoC
{
    internal interface IInitialize
    {
        void OnDependencyInjected();
    }
}
