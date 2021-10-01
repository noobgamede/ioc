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
            DesignByContract.Check.Require(prefabs.ContainsKey(type),"IGameObjectFactory - Invalid Prefab Type");

            GameObject go = Build(prefabs[type][0]);

            Vector3 scale = go.transform.localScale;
            Quaternion rotation = 
        }

        public GameObject Build(GameObject go)
        {
            throw new NotImplementedException();
        }
    }
}
