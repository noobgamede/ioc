using IOC.Context;

namespace IOC.IoC
{
    public class ContextContainer:Container
    {
        IContextNotifier _contextNotifier;

        public ContextContainer(IContextNotifier contextNotifier)
        {
            _contextNotifier = contextNotifier; 
        }

        protected override void OnInstanceGenerated<TContractor>(TContractor instance)
        {
            if (instance is IWaitForFrameInitialization)
                _contextNotifier.AddFrameworkInitializationListener(instance as IWaitForFrameInitialization);

            if (instance is IWaitForFrameDestruction)
                _contextNotifier.AddFrameworkDestructionListener(instance as IWaitForFrameDestruction);
        }
    }
}
