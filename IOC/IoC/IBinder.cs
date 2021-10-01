using System;
namespace IOC.IoC
{
    public interface IBinder<Contractor> where Contractor:class
    {
        void AsSingle<T>(T instance) where T : class, Contractor;
        void AsSingle<T>() where T : Contractor, new();
        void ToFactory<T>(IProvider<T> provider) where T : class, Contractor;
    }
}
