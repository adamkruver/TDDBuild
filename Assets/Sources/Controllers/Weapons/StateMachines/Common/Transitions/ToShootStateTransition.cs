using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Controllers.Weapons.StateMachines.Common.Transitions
{
    public class ToShootStateTransition : TransitionBase
    {
        private readonly IWeapon _weapon;
        private readonly ITargetProvider _targetProvider;
        private readonly IWeaponService _weaponService;
        private readonly IWeaponView _weaponView;

        public ToShootStateTransition(
            IFiniteState nextState,
            IWeapon weapon,
            ITargetProvider targetProvider,
            IWeaponService weaponService,
            IWeaponView weaponView
        ) : base(nextState)
        {
            _weapon = weapon;
            _targetProvider = targetProvider;
            _weaponService = weaponService;
            _weaponView = weaponView;
        }

        protected override bool CanMoveNextState()
        {
            if (_weapon.CanShoot == false)
                return false;

            IEnemyView enemyView = _targetProvider.GetTarget();

            if (enemyView == null)
                return false;

            return _weaponService.HasLockedTarget(
                enemyView, _weaponView.GunPointOffset, _weaponView.GetShootPointPosition()
            );
        }
    }
}