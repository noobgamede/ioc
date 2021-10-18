using System;
using System.Reflection;
namespace IOC.IoC
{
    class SelfProvider<T> : IProvider<T>
    {
        T _instance;
        bool _mustBeInjected = true;
        Type _type;

        public Type Contract => typeof(T);

        public bool Create(Type containerContract,PropertyInfo info,out object instance)
        {
            instance = _instance;
            if(_mustBeInjected == true)
            {
                _mustBeInjected = false;
                return true;
            }
            return false;
        }

        public SelfProvider(T instance)
        {
            _instance = instance;
            _type = typeof(T);
        }
    }
}
