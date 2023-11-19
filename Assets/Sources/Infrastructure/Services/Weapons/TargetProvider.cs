﻿using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Services.Weapons
{
    public class TargetProvider : ITargetProvider
    {
        private readonly ITargetTrackerSystem _targetTrackerSystem;
        private readonly IWeaponView _weaponView;
        private readonly IWeapon _weapon;

        private IEnemyView _enemyView;

        public TargetProvider(
            ITargetTrackerSystem targetTrackerSystem,
            IWeaponView weaponView,
            IWeapon weapon
        )
        {
            _targetTrackerSystem = targetTrackerSystem;
            _weaponView = weaponView;
            _weapon = weapon;
        }

        private bool CanSeeEnemy =>
            _targetTrackerSystem.CanSeeEnemy(
                _weaponView.HeadPosition,
                _enemyView,
                _weapon.MinFireDistance,
                _weapon.MaxFireDistance
            );

        public IEnemyView GetTarget()
        {
            if (CanSeeEnemy == false && TryGetEnemy(out _enemyView) == false)
                return null;

            return _enemyView;
        }

        private bool TryGetEnemy(out IEnemyView enemyView)
        {
            enemyView = _targetTrackerSystem.Track(_weaponView.HeadPosition, _weapon.MinFireDistance,
                _weapon.MaxFireDistance);

            return enemyView != null;
        }
    }
}