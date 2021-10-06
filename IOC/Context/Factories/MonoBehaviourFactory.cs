using UnityEngine;
using System;
namespace IOC.Context
{
    public class MonoBehaviourFactory : Factories.IMonoBehaviourFactory
    {
        IUnityContextHierarchyChangedListener _unityContext;
        public MonoBehaviourFactory(IUnityContextHierarchyChangedListener unityContext)
        {
            _unityContext = unityContext; 
        }
        public M Build<M>(Func<M> constructor) where M : MonoBehaviour
        {
            M mb = constructor();
            _unityContext.OnMonobehaviourAdded(mb);

            GameObject go = mb.gameObject;
            if (go.GetComponent<NotifyComponentsRemoved>() == null)
                go.GetComponent<NotifyComponentsRemoved>().unityContext = _unityContext;
            return mb;
        }
    }
}
