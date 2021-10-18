using System;
using System.Collections.Generic;
using WeakReferenceI = IOC.DataStructures.WeakReference<IOC.Context.IWaitForFrameworkInitialization>;
using WeakReferenceD = IOC.DataStructures.WeakReference<IOC.Context.IWaitForFrameworkDestruction>;
namespace IOC.Context
{
    class ContextNotifier : IContextNotifier
    {
        List<WeakReferenceI> toInitialize;
        List<WeakReferenceD> toDeinitialize;
        public ContextNotifier()
        {
            toInitialize = new List<WeakReferenceI>();
            toDeinitialize = new List<WeakReferenceD>();
        }

        public void AddFrameworkDestructionListener(IWaitForFrameworkDestruction obj)
        {
            if (toDeinitialize != null) toDeinitialize.Add(new WeakReferenceD(obj));
            else
                throw new Exception("An object is expected to be initialized after the framework has been deinitialized. Type: " + obj.GetType());
        }

        public void AddFrameworkInitializationListener(IWaitForFrameworkInitialization obj)
        {
            if (toInitialize != null) toInitialize.Add(new WeakReferenceI(obj));
            else
                throw new Exception("An object is expected to be initialized after the framework has been initialized. Type: " + obj.GetType());

        }

        public void NotifyFrameworkDeinitialized()
        {
            for (int i = toDeinitialize.Count - 1; i >= 0; --i)
                try
                {
                    var obj = toDeinitialize[i];
                    if (obj.IsAlive) obj.Target.OnFrameworkDestroyed();
                }
                catch(Exception e)
                {
                    Utility.Console.LogException(e);
                }
            toDeinitialize = null;
        }

        public void NotifyFrameworkInitialized()
        {
            for(int i=toInitialize.Count-1;i>=0;--i)
                try
                {
                    var obj = toInitialize[i];
                    if (obj.IsAlive) obj.Target.OnFrameworkInitialized();
                }
                catch (Exception e) 
                {
                    Utility.Console.LogException(e);
                }
            toInitialize = null;
        }
    }
}
