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

        private void Update()
        {
            for (int i = 0; i < _ticked.Count; ++i)
                _ticked[i].Tick(Time.deltaTime);
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _lateTicked.Count; ++i)
                _lateTicked[i].LateTick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _physicallyTicked.Count; ++i)
                _physicallyTicked[i].PhysicsTick(Time.fixedDeltaTime);
        }
    }

}
            