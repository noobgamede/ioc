using System;
namespace IOC.DataStructures
{
    public class HashableWeakRef<T> : IEquatable<HashableWeakRef<T>> where T : class
    {
        int _hash;

        WeakReference _weakRef;

        public bool IsAlive { get => _weakRef.IsAlive; }

        public T Target { get => (T)_weakRef.Target; }

        public HashableWeakRef(T target)
        {
            _weakRef = new WeakReference(target);
            _hash = target.GetHashCode();
        }

        public static bool operator !=(HashableWeakRef<T> a, HashableWeakRef<T> b) => !(a == b);

        public static bool operator ==(HashableWeakRef<T> a, HashableWeakRef<T> b)
        {
            if (a._hash != b._hash) return false;
            var tmpTargetA = (T)a._weakRef.Target;
            var tmpTargetB = (T)b._weakRef.Target;
            if (tmpTargetA == null || tmpTargetB == null) return false;
            return tmpTargetA == tmpTargetB;
        }

        public bool Equals(HashableWeakRef<T> other) => this == other;

        public override bool Equals(object other)
        {
            if (other is HashableWeakRef<T>) return this.Equals((HashableWeakRef<T>)other);
            return false;
        }

        public override int GetHashCode() => _hash;
    }
}
