using Sources.Infrastructure.StateMachines.States;
using Sources.InfrastructureInterfaces.StateMachines;
using Sources.Presentation.Views.Zombies;

namespace Sources.Controllers.Zombies.StateMachines.States
{
    public class DestroyState : StateBase
    {
        private readonly IState _nextState;
        private readonly ZombieAfterLifeStateMachine _stateMachine;
        private readonly ZombieView _zombieView;

        public DestroyState(ZombieView zombieView)
        {
            _zombieView = zombieView;
        }

        public override void Enter(object payload)
        {
            _zombieView.Destroy();
        }

        public override void Exit()
        {
        }
    }
}