using Unity.VisualScripting;

namespace Sources.InfrastructureInterfaces.Services
{
    public interface ILateUpdatable
    {
        void UpdateLate(float deltaTime);
    }
}