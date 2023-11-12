using Sources.Infrastructure.StateMachines.States;
using Sources.InfrastructureInterfaces.StateMachines;
using Sources.Presentation.Views.Systems.Damageable;
using Sources.Presentation.Views.Zombies;
using UnityEngine;

namespace Sources.Controllers.Zombies.StateMachines.States
{
    public class FallState : StateBase
    {
        private readonly IState _nextState;
        private readonly ZombieAfterLifeStateMachine _stateMachine;
        private readonly ZombieView _zombieView;
        private readonly DamageableSystemView _damageableSystemView;

        public FallState(
            IState nextState,
            ZombieAfterLifeStateMachine stateMachine,
            ZombieView zombieView
        )
        {
            _nextState = nextState;
            _stateMachine = stateMachine;
            _zombieView = zombieView;
            
            _damageableSystemView = zombieView.DamageableSystemView;
        }

        public override async void Enter(object payload)
        {
            await _zombieView.Fall(CalculateHitProjection());

            _stateMachine.ChangeState(_nextState);
        }

        public override void Exit()
        {
        }
        
        private float CalculateHitProjection()
        {
            float result = Vector3.Dot(_damageableSystemView.LastHitDirection, _zombieView.Forward);

            if (result == 0)
                return 1;

            return result;
        }
    }
}