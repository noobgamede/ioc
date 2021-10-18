using UnityEngine;
using System;
namespace IOC.Context
{
    public class MonoBehaviourFactory : Factories.IMonoBehaviourFactory
    {
        virtual public M Build<M>(Func<M> constructor) where M : MonoBehaviour
        {
            M mb = constructor();
            return mb;
        }
    }
}
