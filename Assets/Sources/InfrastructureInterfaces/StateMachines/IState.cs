using Sources.InfrastructureInterfaces.Services;

namespace Sources.InfrastructureInterfaces.StateMachines
{
    public interface IState : IUpdatable, IFixedUpdatable, ILateUpdatable, IEnterable, IExitable
    {
    }
}