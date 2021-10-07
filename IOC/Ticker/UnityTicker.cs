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

        public void Add(ITickableBase tickable)
        {
            if (tickable is ITickable)
                _ticker.Add(tickable as ITickable);
            if (tickable is IPhysicallyTickable)
                _ticker.AddPhysical(tickable as IPhysicallyTickable);
            if (tickable is ILateTickable)
                _ticker.AddLate(tickable as ILateTickable);
            if (tickable is IIntervaledTickable)
                _ticker.AddIntervaled(tickable as IIntervaledTickable);
        }

        public void Remove(ITickableBase tickable)
        {
            if (tickable is ITickable)
                _ticker.Remove(tickable as ITickable);
            if (tickable is IPhysicallyTickable)
                _ticker.RemovePhysical(tickable as IPhysicallyTickable);
            if (tickable is ILateTickable)
                _ticker.RemoveLate(tickable as ILateTickable);
            if (tickable is IIntervaledTickable)
                _ticker.RemoveIntervaled(tickable as IIntervaledTickable);
        }
    }
}
