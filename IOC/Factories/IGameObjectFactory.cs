using UnityEngine;
namespace IOC.Factories
{
    public interface IGameObjectFactory
    {
        void RegisterPrefab(GameObject prefab,string type,GameObject parent = null);
        GameObject Build(string type);
        GameObject Build(GameObject prefab);
    }
}
