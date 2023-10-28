using Sources.InfrastructureInterfaces.Listeners;

namespace Sources.InfrastructureInterfaces.Services.Pointers
{
    public interface IPointerService
    {
        void RegisterHandler(int pointerId, IPointerHandler handler);
        void UnregisterHandler(int pointerId);
        void RegisterUntouchableHandler(IUntouchablePointerHandler handler);
        void UnregisterUntouchableHandler();
        void UnregisterAll();
    }
}