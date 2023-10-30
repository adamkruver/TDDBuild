namespace Sources.InfrastructureInterfaces.FiniteStateMachines
{
    public interface IFiniteStateChangeService
    {
        void ChangeState(IFiniteState nextState);
    }
}