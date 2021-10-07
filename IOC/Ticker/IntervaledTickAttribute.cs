using System;
namespace IOC.Ticker
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    internal class IntervaledTickAttribute:Attribute
    {
        public float interval;

        public IntervaledTickAttribute(float intervalTime)
        {
            interval = intervalTime; 
        }
    }
}
