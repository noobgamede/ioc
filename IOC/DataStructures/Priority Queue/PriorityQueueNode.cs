using System;
namespace IOC.DataStructures
{
    public class PriorityQueueNode
    {
        public double Priority { get; set; }
        public long InsertionIndex { get; set; }
        public int QueueIndex { get; set; }
    }
}
