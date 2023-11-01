using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.Infrastructure.Services.Weapons;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using UnityEngine;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.Transitions
{
    public class ToShootStateTransition : TransitionBase
    {
        private readonly IWeapon _weapon;
        private readonly ITargetTrackerSystem _targetTrackerSystem;
        private readonly WeaponService _weaponService;

        public ToShootStateTransition(
            IFiniteState nextState,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            WeaponService weaponService
        ) : base(nextState)
        {
            _weapon = weapon;
            _targetTrackerSystem = targetTrackerSystem;
            _weaponService = weaponService;
        }

        protected override bool CanMoveNextState()
        {
            if(_weapon.CanShoot == false)
                return false;
            
            IEnemyView enemyView = _targetTrackerSystem.Track(_weapon.MaxFireDistance);

            if (enemyView == null)
                return false;
            
            return _weaponService.HasLockedTarget(enemyView);
        }
    }
}