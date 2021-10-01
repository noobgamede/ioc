using System;
using UnityEngine;
namespace IOC.Ticker
{
    public class UnityTicker : ITicker
    {
        private TickBehaviour _ticker;

        public UnityTicker()
        {
            _ticker = GameObject.FindObjectOfType<TickBehaviour>();
             if(_ticker==null)
            {
                GameObject go = new GameObject("IOCTicker");
                _ticker = go.AddComponent<TickBehaviour>(); 
            }
        }

        public void Add(ITickable tickable)
        {
            _ticker.Add(tickable);
        }

        public void AddLate(ILateTickable tickable)
        {
            _ticker.AddLate(tickable);
        }

        public void AddPhysical(IPhysicallyTickable tickable)
        {
            _ticker.AddPhysical(tickable);
        }

        public void Remove(ITickable tickable)
        {
            _ticker.Remove(tickable);
        }

        public void RemoveLate(ILateTickable tickable)
        {
            _ticker.RemoveLate(tickable);
        }

        public void RemovePhysical(IPhysicallyTickable tickable)
        {
            _ticker.RemovePhysical(tickable);
        }
    }
}
