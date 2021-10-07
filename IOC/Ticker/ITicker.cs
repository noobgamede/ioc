using System;
namespace IOC.Ticker
{
    public interface ITicker
    {
        void Add(ITickableBase tickable);
        void Remove(ITickableBase tickable);
    }
}
