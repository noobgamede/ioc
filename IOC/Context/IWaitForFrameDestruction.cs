using System;
namespace IOC.Context
{
    public interface IWaitForFrameDestruction
    {
        void OnFrameworkDestroyed();
    }
}
