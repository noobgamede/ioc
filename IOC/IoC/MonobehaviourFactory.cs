using UnityEngine;
using System;
namespace IOC.IoC
{
    public class MonobehaviourFactory : IMonobehaviourFactory
    {
        IContainer _container;
        public MonobehaviourFactory(IContainer container)
        {
            _container = container; 
        }
        public M Build<M>(Func<M> constructor) where M : Monobehaviour
        {
            M mb = (M)constructor();
            _container.Inject(mb);
            return mb;
        }
    }
}
