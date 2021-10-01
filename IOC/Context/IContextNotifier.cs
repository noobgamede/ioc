using System;
namespace IOC.Context
{
    public interface IContextNotifier
    {
        void NotifyFrameworkInitialized();
        void NotifyFrameworkDeinitialized();

        void AddFrameworkInitializationListener(IWaitForFrameInitialization obj);
        void AddFrameworkDestructionListener(IWaitForFrameDestruction obj);
    }
}
