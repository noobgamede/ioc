using System;
namespace IOC.Context
{
    public interface IWaitForFrameInitialization
    {
        void OnFrameworkInitialized();
    }
}
