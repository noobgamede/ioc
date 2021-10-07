using System;
namespace IOC.IoC
{
    public class MultiProvider<T> : IProvider<T> where T : new()
    {
        public Type Contract => typeof(T);

        public bool Single => false;

        public object Create(Type containerContract)
        {
            return new T();
        }
    }
}
