using System;
using JetBrains.Annotations;
using Sources.Domain.Weapons;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using UnityEngine;

namespace Sources.Controllers.Weapons.StateMachines.Lasers.States
{
    public class TrackTargetState : FiniteStateBase
    {
        private readonly IWeapon _weapon;
        private readonly ITargetTrackerSystem _targetTrackerSystem;
        private readonly IWeaponService _weaponService;
        private readonly ICompositeWeaponView _compositeWeaponView;

        private float _gunPointOffset;
        private IEnemyView _enemy;

        public TrackTargetState(
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            IWeaponService weaponService,
            ICompositeWeaponView compositeWeaponView
        )
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _targetTrackerSystem = targetTrackerSystem ?? throw new ArgumentNullException(nameof(targetTrackerSystem));
            _weaponService = weaponService ?? throw new ArgumentNullException(nameof(weaponService));
            _compositeWeaponView = compositeWeaponView ?? throw new ArgumentNullException(nameof(compositeWeaponView));
        }

        private bool CanSeeEnemy =>
            _targetTrackerSystem.CanSeeEnemy(
                _compositeWeaponView.HeadPosition, _enemy, _weapon.MinFireDistance, _weapon.MaxFireDistance
            );

        protected override void OnEnter()
        {
            _gunPointOffset = _compositeWeaponView.GunPointOffset;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if(CanSeeEnemy == false && TryGetEnemy(out _enemy) == false)
                return;

            _weaponService.UpdateLookDirectionWithPredict(_enemy, _weapon.HorizontalRotationSpeed, _gunPointOffset);
        }

        private bool TryGetEnemy(out IEnemyView view)
        {
            view = _targetTrackerSystem.Track(_compositeWeaponView.HeadPosition, _weapon.MinFireDistance, _weapon.MaxFireDistance);
            
            return view != null;
        }
    }
}