using UnityEngine;
using IOC.IoC;

public class UnityRoot : MonoBehaviour
{
}

public class UnityRoot<T> : UnityRoot where T:class,ICompositionRoot,new() 
{
    T _applicationRoot;

    private void Awake()
    {
        _applicationRoot = new T();
        Init();
    }

    virtual protected void Init()
    {
        MonoBehaviour[] behaviours = this.transform.GetComponentsInChildren<MonoBehaviour>(true);
        for(int i=0;i<behaviours.Length;++i)
        {
            _applicationRoot.container.Inject(behaviours[i]); 
        }
    }
}