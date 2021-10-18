using System.Collections.Generic;
using UnityEngine;

namespace IOC.Context
{
    public class GameObjectFactory:Factories.IGameObjectFactory
    {
        Dictionary<string, GameObject[]> prefabs;

        public GameObjectFactory()
        {
            prefabs = new Dictionary<string, GameObject[]>();
        }

        public void RegisterPrefab(GameObject prefab, string prefabName, GameObject parent=null)
        {
            GameObject[] objects = new GameObject[2];
            objects[0] = prefab;
            objects[1] = parent;
            prefabs.Add(prefabName, objects);
        }

        public GameObject Build(string prefabName)
        {
            DesignByContract.Check.Require(prefabs.ContainsKey(prefabName),"IGameObjectFactory - prefab was not found: "+prefabName);
            GameObject go = Build(prefabs[prefabName][0]);

            GameObject parent = prefabs[prefabName][1];
            if(parent!=null)
            {
                Vector3 scale = go.transform.localScale;
                Quaternion rotation = go.transform.localRotation;
                Vector3 position = go.transform.localPosition;

                parent.SetActive(true);

                go.transform.parent = parent.transform;

                go.transform.localPosition = position;
                go.transform.localRotation = rotation;
                go.transform.localScale = scale;
            }
            return go;
        }

        virtual public GameObject Build(GameObject go)
        {
            GameObject copy = Object.Instantiate(go) as GameObject;
            return copy;
        }
    }
}
