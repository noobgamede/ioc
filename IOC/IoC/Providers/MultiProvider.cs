using System;
using System.Reflection;

namespace IOC.IoC
{
    class MultiProvider<T> : IProvider<T> where T : new()
    {
        Type _type;

        public MultiProvider()
        {
            _type = typeof(T); 
        }

        public Type Contract => _type;


        public bool Create(Type containerContract,PropertyInfo info,out object instance)
        {
            instance = new T();
            return true;
        }
    }
}
