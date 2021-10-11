using System;
using System.Runtime.Serialization;
namespace IOC.DataStructures 
{
    [Serializable]
    public class WeakReference<T> : WeakReference where T : class
    {
        public bool IsValid { get { return Target != null && IsAlive == true; } }
        public WeakReference(T target) : base(target) { }
        public WeakReference(T target, bool trackResurrection) : base(target, trackResurrection) { }

#if !NETFX_CORE
        protected WeakReference(SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif

        public new T Target
        {
            get
            {
                return (T)base.Target;
            }
            set
            {
                base.Target = value;
            }
        }
    }

    public static class WeakReferenceUtility
    {
        public static bool IsValid(this WeakReference obj)
        {
            return obj != null && obj.IsAlive == true && obj.Target != null;
        }
    }
}
