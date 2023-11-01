using Sources.InfrastructureInterfaces.Services;

namespace Sources.Controllers
{
    public interface IPresenter : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        void Enable();
        void Disable();
    }
}