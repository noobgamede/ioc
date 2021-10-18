namespace IOC.Context
{
    public interface ICompositionRoot
    {
        void OnContextCreated(UnityContext contextHolder);
        void OnContextInitialized();
        void OnContextDestroyed();
    }
}
