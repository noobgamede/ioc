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
            if (instance is IWaitForFrameworkInitialization)
                _contextNotifier.AddFrameworkInitializationListener(instance as IWaitForFrameworkInitialization);

            if (instance is IWaitForFrameworkDestruction)
                _contextNotifier.AddFrameworkDestructionListener(instance as IWaitForFrameworkDestruction);
        }
    }
}
