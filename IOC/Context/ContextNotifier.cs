using System;
using System.Collections.Generic;
namespace IOC.Context
{
    public class ContextNotifier : IContextNotifier
    {
        List<WeakReference<IWaitForFrameInitialization>> toInitialize;
        List<WeakReference<IWaitForFrameDestruction>> toDeinitialize;
        public ContextNotifier()
        {
            toInitialize = new List<WeakReference<IWaitForFrameInitialization>>();
            toDeinitialize = new List<WeakReference<IWaitForFrameDestruction>>(); 
        }

        public void AddFrameworkDestructionListener(IWaitForFrameDestruction obj)
        {
            if (toDeinitialize != null) toDeinitialize.Add(new WeakReference<IWaitForFrameDestruction>(obj));
            else
                throw new Exception("An object is expected to be initialized after the framework has been deinitialized. Type: " + obj.GetType());
        }

        public void AddFrameworkInitializationListener(IWaitForFrameInitialization obj)
        {
            if (toInitialize != null) toInitialize.Add(new WeakReference<IWaitForFrameInitialization>(obj));
            else
                throw new Exception("An object is expected to be initialized after the framework has been initialized. Type: " + obj.GetType());

        }

        public void NotifyFrameworkDeinitialized()
        {
            for (int i = toDeinitialize.Count - 1; i >= 0; --i)
            {
                var obj = toDeinitialize[i];
                if (obj.IsAlive) (obj.Target as IWaitForFrameDestruction).OnFrameworkDestroyed();
            }
            toDeinitialize = null;
        }

        public void NotifyFrameworkInitialized()
        {
            for(int i=toInitialize.Count-1;i>=0;--i)
            {
                var obj = toInitialize[i];
                if (obj.IsAlive) (obj.Target as IWaitForFrameInitialization).OnFrameworkInitialized();
            }
            toInitialize = null;
        }
    }
}
