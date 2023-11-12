using Sources.Infrastructure.StateMachines;
using Sources.InfrastructureInterfaces.StateMachines;

namespace Sources.Controllers.Zombies.StateMachines
{
    public class ZombieAfterLifeStateMachine : StateMachine<IState>, IZombieStateMachine
    {
        private IState _firstState;

        public void SetFirstState(IState firstState) => 
            _firstState = firstState;

        public void Enable() =>
            EnterState(_firstState);

        public void Disable() =>
            ExitState();
    }
}