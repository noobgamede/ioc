using UnityEngine;
namespace IOC.Context
{
    public class NotifyComponentsRemoved : MonoBehaviour
    {
        public IUnityContextHierarchyChangedListener unityContext { private get; set; }

        private void OnDestroy()
        {
            MonoBehaviour[] components = gameObject.GetComponentsInChildren<MonoBehaviour>(true);

            for(int i =0;i<components.Length;++i)
            {
                if (components[i] != null) unityContext.OnMonobehaviourRemoved(components[i]); 
            }
        }
    }

    public class NotifyEntityRemoved : MonoBehaviour
    {
        public IUnityContextHierarchyChangedListener unityContext { private get; set; }

        private void OnDestroy()
        {
            unityContext.OnGameObjectRemoved(gameObject);
        }
    }
}
