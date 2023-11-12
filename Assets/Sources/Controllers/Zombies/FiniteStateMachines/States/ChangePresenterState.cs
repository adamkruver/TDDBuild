using Sources.Controllers.Zombies.StateMachines;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.StateMachines;
using Sources.Presentation.Views.Zombies;

namespace Sources.Controllers.Zombies.FiniteStateMachines.States
{
    public class ChangePresenterState : FiniteStateBase
    {
        private readonly IState _nextState;
        private readonly ZombieAfterLifeStateMachine _stateMachine;
        private readonly ZombieView _zombieView;
        private readonly ZombieAfterLifeStateMachineFactory _zombieAfterLifeStateMachineFactory;

        public ChangePresenterState(
            ZombieView zombieView,
            ZombieAfterLifeStateMachineFactory zombieAfterLifeStateMachineFactory
        )
        {
            _zombieView = zombieView;
            _zombieAfterLifeStateMachineFactory = zombieAfterLifeStateMachineFactory;
        }

        protected override void OnEnter() =>
            _zombieView.Construct(_zombieAfterLifeStateMachineFactory.Create(_zombieView));
    }
}