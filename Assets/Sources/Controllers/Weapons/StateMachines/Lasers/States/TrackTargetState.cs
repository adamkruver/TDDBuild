using System;
using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class TrackTargetState : FiniteStateBase
    {
        private readonly IWeapon _weapon;
        private readonly IWeaponService _weaponService;
        private readonly ICompositeWeaponView _compositeWeaponView;
        private readonly ITargetProvider _targetProvider;

        private float _gunPointOffset;
        private IEnemyView _enemy;

        public TrackTargetState(
            IWeapon weapon,
            IWeaponService weaponService,
            ICompositeWeaponView compositeWeaponView,
            ITargetProvider targetProvider
        )
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _weaponService = weaponService ?? throw new ArgumentNullException(nameof(weaponService));
            _compositeWeaponView = compositeWeaponView ?? throw new ArgumentNullException(nameof(compositeWeaponView));
            _targetProvider = targetProvider ?? throw new ArgumentNullException(nameof(targetProvider));
        }

        protected override void OnEnter()
        {
            _gunPointOffset = _compositeWeaponView.GunPointOffset;
        }

        protected override void OnUpdate(float deltaTime)
        {
            _enemy = _targetProvider.GetTarget();
            
            if (_enemy == null)
                return;

            _weaponService.UpdateLookDirectionWithPredict(_enemy, _weapon.HorizontalRotationSpeed, _gunPointOffset);
        }
    }
}