using System;
namespace IOC.IoC 
{
    public interface IProvider
    {
        object Create(Type containerContract);
        Type Contract { get; }
        bool Single { get; }
    }

    public interface IProvider<T> : IProvider { }

    public class StandardProvider<T> : IProvider<T> where T : new()
    {
        public Type Contract => typeof(T);

        public bool Single => true;

        public object Create(Type containerContract)
        {
            return new T();
        }
    }

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