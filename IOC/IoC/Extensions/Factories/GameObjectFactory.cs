using UnityEngine;
namespace IOC.IoC
{
    public class GameObjectFactory:Context.GameObjectFactory
    {
        IContainer _container;

        public GameObjectFactory(IContainer container):base()
        {
            _container = container;
        }

        public override GameObject Build(GameObject prefab)
        {
            var copy = base.Build(prefab);
            var monobehaviours = copy.GetComponentsInChildren<MonoBehaviour>();
            for(int i = 0; i < monobehaviours.Length; ++i) 
            {
                _container.Inject(monobehaviours[i]); 
            }
            return copy;
        }
    }
}
