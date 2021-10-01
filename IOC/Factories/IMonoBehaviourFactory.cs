using System;
using UnityEngine;
namespace IOC.Factories
{
    public interface IMonoBehaviourFactory
    {
        M Build<M>(Func<M> constructor) where M : MonoBehaviour;
    }
}
