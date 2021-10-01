using System;
namespace IOC.Context
{
    public interface ICompositionRoot
    {
        void OnContextInitialized();
        void OnContextDestroyed();
    }
}
