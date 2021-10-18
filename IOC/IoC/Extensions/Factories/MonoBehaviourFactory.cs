using System;
namespace IOC.IoC
{
    public class MonoBehaviourFactory:Context.MonoBehaviourFactory
    {
        IContainer _container;

        public MonoBehaviourFactory(IContainer container):base()
        {
            _container = container;
        }

        public override M Build<M>(Func<M> constructor)
        {
            var copy = base.Build(constructor);
            _container.Inject(copy);
            return copy;
        }
    }
}
