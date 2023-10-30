using Sources.InfrastructureInterfaces.Services;

namespace Sources.InfrastructureInterfaces.FiniteStateMachines
{
    public interface IFiniteState : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        void Enter(IFiniteStateChangeService service);
        void Exit();
    }
}