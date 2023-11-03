using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.Transitions
{
    public class ToShootStateTransition : TransitionBase
    {
        private readonly IWeapon _weapon;
        private readonly ITargetTrackerSystem _targetTrackerSystem;
        private readonly IWeaponService _weaponService;

        public ToShootStateTransition(
            IFiniteState nextState,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            IWeaponService weaponService
        ) : base(nextState)
        {
            _weapon = weapon;
            _targetTrackerSystem = targetTrackerSystem;
            _weaponService = weaponService;
        }

        protected override bool CanMoveNextState()
        {
            if (_weapon.CanShoot == false)
                return false;

            IEnemyView enemyView = _targetTrackerSystem.Track(_weapon.MaxFireDistance);

            if (enemyView == null)
                return false;

            return _weaponService.HasLockedTarget(enemyView);
        }
    }
}