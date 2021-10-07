using System;
namespace IOC.IoC
{
    public class StandardProvider<T> : IProvider<T> where T : new()
    {
        public Type Contract => typeof(T);

        public bool Single => true;

        public object Create(Type containerContract)
        {
            return new T();
        }
    }
}
