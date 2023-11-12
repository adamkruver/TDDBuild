using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.Transitions
{
    public class ToShootStateTransition : TransitionBase
    {
        private readonly IWeapon _weapon;
        private readonly ITargetProvider _targetProvider;
        private readonly IWeaponService _weaponService;
        private readonly ICompositeWeaponView _compositeWeaponView;

        public ToShootStateTransition(
            IFiniteState nextState,
            IWeapon weapon,
            ITargetProvider targetProvider,
            IWeaponService weaponService,
            ICompositeWeaponView compositeWeaponView
        ) : base(nextState)
        {
            _weapon = weapon;
            _targetProvider = targetProvider;
            _weaponService = weaponService;
            _compositeWeaponView = compositeWeaponView;
        }

        protected override bool CanMoveNextState()
        {
            if (_weapon.CanShoot == false)
                return false;

            IEnemyView enemyView = _targetProvider.GetTarget();

            if (enemyView == null)
                return false;

            return _weaponService.HasLockedTarget(
                enemyView, _compositeWeaponView.GunPointOffset, _compositeWeaponView.GetShootPoint(_weapon.BulletId)
            );
        }
    }
}