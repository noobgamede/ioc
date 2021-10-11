using System;
namespace IOC.Context
{
    public interface IContextNotifier
    {
        void NotifyFrameworkInitialized();
        void NotifyFrameworkDeinitialized();

        void AddFrameworkInitializationListener(IWaitForFrameworkInitialization obj);
        void AddFrameworkDestructionListener(IWaitForFrameworkDestruction obj);
    }
}
