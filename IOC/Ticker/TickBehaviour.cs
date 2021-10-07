﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System;

namespace IOC.Ticker
{
    public class TickBehaviour : MonoBehaviour
    {
        List<ITickable> _ticked = new List<ITickable>();
        List<IPhysicallyTickable> _physicallyTicked = new List<IPhysicallyTickable>();
        List<ILateTickable> _lateTicked = new List<ILateTickable>();
        Dictionary<IIntervaledTickable, IEnumerator> _intervalledTicked = 
        new Dictionary<IIntervaledTickable, IEnumerator>();

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

        internal void AddIntervaled(IIntervaledTickable tickable)
        {
            var methodInfo = ((Action)tickable.IntervaledTick).Method;
            object[] attrs = methodInfo.GetCustomAttributes(typeof(IntervaledTickAttribute),true);
            IEnumerator intervaledTick = IntervaledUpdate(tickable,(attrs[0]as IntervaledTickAttribute).interval);
            StartCoroutine(intervaledTick); 
        }

        internal void RemoveIntervaled(IIntervaledTickable tickable)
        {
            IEnumerator enumerator;
            if(_intervalledTicked.TryGetValue(tickable,out enumerator))
            {
                StopCoroutine(enumerator);
                _intervalledTicked.Remove(tickable); 
            }
        }

        private void Update()
        {
            for (int i = _ticked.Count-1; i >=0; --i)
                _ticked[i].Tick(Time.deltaTime);
        }

        private void LateUpdate()
        {
            for (int i = _lateTicked.Count-1; i >=0; --i)
                _lateTicked[i].LateTick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            for (int i = _physicallyTicked.Count-1; i >=0 ; --i)
                _physicallyTicked[i].PhysicsTick(Time.fixedDeltaTime);
        }

        IEnumerator IntervaledUpdate(IIntervaledTickable tickable,float seconds)
        {
            while(true)
            {
                DateTime next = DateTime.UtcNow.AddSeconds(seconds);
                while (DateTime.UtcNow < next)
                    yield return null;
                tickable.IntervaledTick(); 
            }
        }
    }

}
            