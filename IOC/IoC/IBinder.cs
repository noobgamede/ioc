using System;
namespace IOC.IoC
{
    public interface IBinder<Contractor> where Contractor:class
    {
        void AsInstance<T>(T instance) where T : class, Contractor;
        void AsSingle<T>() where T : Contractor, new();
        void ToProvider<T>(IProvider<T> provider) where T : class, Contractor;
        void Bind<ToBind>(IInternalContainer container) where ToBind : class;
    }
}
