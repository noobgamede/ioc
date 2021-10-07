using System;
namespace IOC.Ticker
{
    public interface ITickableBase { }

    public interface ITickable:ITickableBase
    {
        void Tick(float deltaSec);
    }

    public interface ILateTickable: ITickableBase
    {
        void LateTick(float deltaSec); 
    }

    public interface IPhysicallyTickable: ITickableBase
    {
        void PhysicsTick(float deltaTick); 
    }

    public interface IIntervaledTickable:ITickableBase
    {
        void IntervaledTick(); 
    }
}
