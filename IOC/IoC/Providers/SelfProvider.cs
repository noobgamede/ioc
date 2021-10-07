using System;
namespace IOC.IoC
{
    public class SelfProvider<T> : IProvider<T>
    {
        T _instance;
        public Type Contract => typeof(T);

        public bool Single => true;

        public object Create(Type containerContract)
        {
            return _instance;
        }

        public SelfProvider(T instance)
        {
            _instance = instance;
        }
    }
}
