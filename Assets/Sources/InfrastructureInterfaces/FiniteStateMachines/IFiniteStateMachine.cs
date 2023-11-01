using Sources.InfrastructureInterfaces.Services;

namespace Sources.InfrastructureInterfaces.FiniteStateMachines
{
    public interface IFiniteStateMachine : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
        void Run();
        void Stop();
    }
}