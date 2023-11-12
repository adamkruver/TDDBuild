using Sources.Infrastructure.StateMachines.States;
using Sources.InfrastructureInterfaces.StateMachines;
using Sources.Presentation.Views.Zombies;

namespace Sources.Controllers.Zombies.StateMachines.States
{
    public class DecayState : StateBase
    {
        private readonly IState _nextState;
        private readonly ZombieAfterLifeStateMachine _stateMachine;
        private readonly ZombieView _zombieView;

        public DecayState(
            IState nextState,
            ZombieAfterLifeStateMachine stateMachine,
            ZombieView zombieView
        )
        {
            _nextState = nextState;
            _stateMachine = stateMachine;
            _zombieView = zombieView;
        }

        public override async void Enter(object payload)
        {
            await _zombieView.Decay();
            _stateMachine.ChangeState(_nextState);
        }

        public override void Exit()
        {
        }
    }
}