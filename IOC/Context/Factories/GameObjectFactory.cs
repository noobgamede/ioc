using System.Collections.Generic;
using UnityEngine;

namespace IOC.Context
{
    public class GameObjectFactory:Factories.IGameObjectFactory
    {
        IUnityContextHierarchyChangedListener _unityContext;
        Dictionary<string, GameObject[]> prefabs;
        public GameObjectFactory(IUnityContextHierarchyChangedListener root)
        {
            _unityContext = root;
            prefabs = new Dictionary<string, GameObject[]>();
        }

        public void RegisterPrefab(GameObject prefab, string type, GameObject parent)
        {
            GameObject[] objects = new GameObject[2];
            objects[0] = prefab;
            objects[1] = parent;
            prefabs.Add(type, objects);
        }

        public GameObject Build(string type)
        {
            //DesignerByContract.Check.Require(prefabs.ContainsKey(type),"IGameObjectFactory - Invalid Prefab Type");
            GameObject go = Build(prefabs[type][0]);

            Vector3 scale = go.transform.localScale;
            Quaternion rotation = go.transform.localRotation;
            Vector3 position = go.transform.localPosition;

            prefabs[type][1].transform.gameObject.SetActive(true);

            go.transform.parent = prefabs[type][1].transform;

            go.transform.localPosition = position;
            go.transform.localRotation = rotation;
            go.transform.localScale = scale;

            return go;
        }

        public GameObject Build(GameObject go)
        {
            GameObject copy = Object.Instantiate(go) as GameObject;
            MonoBehaviour[] components = copy.GetComponentsInChildren<MonoBehaviour>(true);
            for(int i=0;i<components.Length;++i)
            {
                if(components[i]!=null)
                _unityContext.OnMonobehaviourAdded(components[i]); 
            }
            _unityContext.OnGameObjectAdded(copy);

            copy.AddComponent<NotifyComponentsRemoved>().unityContext = _unityContext;
            copy.AddComponent<NotifyEntityRemoved>().unityContext = _unityContext;
            return copy;
        }
    }
}
