using System;
using UnityEngine;
namespace IOC.IoC
{
    public interface IMonobehaviourFactory
    {
        M Build<M>(Func<M> constructor) where M : MonoBehaviour;
    }
}
