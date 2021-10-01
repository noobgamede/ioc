using System;
using UnityEngine;
namespace IOC.IoC
{
    public interface IMonoBehaviourFactory
    {
        M Build<M>(Func<M> constructor) where M : MonoBehaviour;
    }
}
