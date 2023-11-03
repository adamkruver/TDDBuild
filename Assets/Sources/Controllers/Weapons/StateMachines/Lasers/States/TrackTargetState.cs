using System;
using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class TrackTargetState : FiniteStateBase
    {
        private readonly IWeapon _weapon;
        private readonly ITargetTrackerSystem _targetTrackerSystem;
        private readonly IWeaponService _weaponService;

        public TrackTargetState(
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            IWeaponService weaponService
        )
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _targetTrackerSystem = targetTrackerSystem ?? throw new ArgumentNullException(nameof(targetTrackerSystem));
            _weaponService = weaponService ?? throw new ArgumentNullException(nameof(weaponService));
        }

        protected override void OnUpdate(float deltaTime)
        {
            IEnemyView enemyView = _targetTrackerSystem.Track(_weapon.MaxFireDistance);

            if (enemyView == null)
                return;

            _weaponService.UpdateLookDirectionWithPredict(enemyView, _weapon.HorizontalRotationSpeed);
        }
    }
}