using System;
namespace IOC.Context
{
    public interface IWaitForFrameworkDestruction
    {
        void OnFrameworkDestroyed();
    }
}
