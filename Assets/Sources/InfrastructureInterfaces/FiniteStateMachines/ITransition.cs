using Sources.InfrastructureInterfaces.Services;

namespace Sources.InfrastructureInterfaces.FiniteStateMachines
{
    public interface ITransition : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        void Enable();
        void Disable();
        bool HasNextState(out IFiniteState nextState);
    }
}