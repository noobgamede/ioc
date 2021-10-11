using System;
namespace IOC.Context
{
    public interface IWaitForFrameworkInitialization
    {
        void OnFrameworkInitialized();
    }
}
