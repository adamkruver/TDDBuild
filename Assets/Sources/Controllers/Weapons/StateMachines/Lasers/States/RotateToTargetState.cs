using System;
using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.Infrastructure.Services.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class RotateToTargetState : FiniteStateBase
    {
        private readonly IWeapon _weapon;
        private readonly ITargetTrackerSystem _targetTrackerSystem;
        private readonly WeaponService _weaponService;

        public RotateToTargetState(
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            WeaponService weaponService
        )
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _targetTrackerSystem = targetTrackerSystem ?? throw new ArgumentNullException(nameof(targetTrackerSystem));
            _weaponService = weaponService ?? throw new ArgumentNullException(nameof(weaponService));
        }

        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate(float deltaTime)
        {
            IEnemyView enemyView = _targetTrackerSystem.Track(_weapon.FireDistance);
            _weaponService.LookWithPredict(enemyView);
        }
    }
}