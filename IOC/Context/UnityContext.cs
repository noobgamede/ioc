using System.Collections;
using IOC.Context;
using UnityEngine;

public abstract class UnityContext : MonoBehaviour
{
    protected abstract void OnAwake();

    private void Awake()
    {
        OnAwake();
    }
}

public class UnityContext<T> : UnityContext where T:class,ICompositionRoot,new() 
{
    T _applicationRoot;

    protected override void OnAwake()
    {
        _applicationRoot = new T();
        _applicationRoot.OnContextCreated(this);
    }

    private void OnDestroy()
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