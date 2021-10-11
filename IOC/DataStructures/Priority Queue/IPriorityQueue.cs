using System.Collections.Generic;
namespace IOC.DataStructures
{
    public interface IPriorityQueue<T>:IEnumerable<T> where T:PriorityQueueNode
    {
        void Remove(T node);
        void UpdatePriority(T node, double priority);
        void Enqueue(T node,double priority);
        T Dequeue();
        T First { get; }
        int Count { get; }
        int MaxSize { get; }
        void Clear();
        bool Contains(T node);
    }
}
