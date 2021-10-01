using System;
namespace IOC.IoC
{
    public interface IContainer
    {
        IBinder<TContractor> Bind<TContractor>() where TContractor : class;
        void BindSelf<TContractor>() where TContractor : class, new();
        TContractor Build<TContractor>() where TContractor : class;
        void Release<TContractor>() where TContractor : class;
        TContractor Inject<TContractor>(TContractor instance);
    }

    public interface IInternalContainer
    {
        void Register<T, K>(System.Type type, K provider) where K : IProvider<T>; 
    }
}
