using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.Presentation.Views.Systems.Damageable;
using Sources.Presentation.Views.Zombies;
using UnityEngine;

namespace Sources.Controllers.Zombies.StateMachines.States
{
    public class HitState : FiniteStateBase
    {
        private readonly ZombieView _zombieView;
        private readonly DamageableSystemView _damageableSystemView;

        public HitState(ZombieView zombieView)
        {
            _zombieView = zombieView;
            _damageableSystemView = zombieView.DamageableSystemView;
        }

        protected override void OnEnter()
        {
            _zombieView.Hit(CalculateHitProjection());
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