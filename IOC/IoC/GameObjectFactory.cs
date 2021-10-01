using System;
using System.Collections.Generic;
using UnityEngine;

namespace IOC.IoC
{
    public class GameObjectFactory:IGameObjectFactory
    {
        IContainer _container;
        Dictionary<string, GameObject[]> prefabs;
        public GameObjectFactory(IContainer container)
        {
            _container = container;
            prefabs = new Dictionary<string, GameObject[]>();
        }

        public void AddPrefab(GameObject prefab, string type, GameObject parent)
        {
            GameObject[] objects = new GameObject[2];
            objects[0] = prefab;
            objects[1] = parent;
            prefabs.Add(type, objects);
        }

        public GameObject Build(string type)
        {
            throw new NotImplementedException();
        }

        public GameObject Build(GameObject go)
        {
            throw new NotImplementedException();
        }
    }
}
