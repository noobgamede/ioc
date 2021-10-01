using UnityEngine;
using IOC.Context;
using System;
using System.Collections;

public class UnityContext : MonoBehaviour
{
}

public class UnityContext<T> : UnityContext where T:class,ICompositionRoot,IUnityContextHierarchyChangedListener,new() 
{
    T _applicationRoot;

    virtual protected void Awake()
    {
        Init();
    }

    void Init()
    {
        _applicationRoot = new T();
        MonoBehaviour[] behaviours = transform.GetComponentsInChildren<MonoBehaviour>(true);
        for(int i=0;i<behaviours.Length;++i)
        {
            if(behaviours[i]!=null)
            _applicationRoot.OnMonobehaviourAdded(behaviours[i]); 
        }
        Transform[] children = transform.GetComponentsInChildren<Transform>(true);

        for(int i=0;i<children.Length;++i)
        {
            if (children[i] != null)
                _applicationRoot.OnGameObjectAdded(children[i].gameObject); 
        }
    }

    private void OnDestroy()
    {
        FrameworkDestroyed();
    }

    private void FrameworkDestroyed()
    {
        _applicationRoot.OnContextDestroyed();
    }

    private void Start()
    {
        if (Application.isPlaying)
            StartCoroutine(WaitForFrameworkInitialization());
    }

    IEnumerator WaitForFrameworkInitialization()
    {
        yield return new WaitForEndOfFrame();
        _applicationRoot.OnContextInitialized(); 
    }
}