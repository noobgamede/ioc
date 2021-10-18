using System;
using System.Reflection;

namespace IOC.IoC
{
    class StandardProvider<T> : IProvider<T> where T : new()
    {
        T _object;
        Type _type;

        public StandardProvider()
        {
            _type = typeof(T); 
        }

        public Type Contract => _type;

        public bool Create(Type containerContract,PropertyInfo info,out object instance)
        {
            bool mustInject = false;
            if(_object==null)
            {
                _object = new T();
                mustInject = true; 
            }
            instance = _object;
            return mustInject;
        }
    }
}
