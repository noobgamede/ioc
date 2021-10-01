using UnityEngine;
using System.Collections.Generic;
namespace IOC.Ticker
{
    public class TickBehaviour : MonoBehaviour
    {
        List<ITickable> _ticked = new List<ITickable>();
        List<IPhysicallyTickable> _physicallyTicked = new List<IPhysicallyTickable>();
        List<ILateTickable> _lateTicked = new List<ILateTickable>();

        internal void Add(ITickable tickable)
        {
            _ticked.Add(tickable);
        }

        internal void Remove(ITickable tickable)
        {
            _ticked.Remove(tickable); 
        }

        internal void AddPhysical(IPhysicallyTickable tickable)
        {
            _physicallyTicked.Add(tickable);
        }

        internal void RemovePhysical(IPhysicallyTickable tickable)
        {
            _physicallyTicked.Remove(tickable);
        }

        internal void AddLate(ILateTickable tickable)
        {
            _lateTicked.Add(tickable);
        }

        internal void RemoveLate(ILateTickable tickable)
        {
            _lateTicked.Remove(tickable);
        }
    }

}
            