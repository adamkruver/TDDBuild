using System;
using Sources.Domain.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Infrastructure.Services.Weapons
{
    public class WeaponService
    {
        private readonly IWeapon _weapon;
        private readonly IWeaponRotationSystem _weaponRotationSystem;

        public WeaponService(IWeapon weapon, IWeaponRotationSystem weaponRotationSystem)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));

            _weaponRotationSystem =
                weaponRotationSystem ?? throw new ArgumentNullException(nameof(weaponRotationSystem));
        }

        public void LookWithPredict(IEnemyView enemy)
        {
            Vector3 directionToEnemyNormalized = (enemy.Position - _weaponRotationSystem.Position).normalized;
            Vector3 enemyForward = enemy.Forward;

            Vector3 enemyOrthogonal =
                Vector3.Dot(directionToEnemyNormalized, enemyForward) * directionToEnemyNormalized;
            Vector3 enemyTangent = enemyForward - enemyOrthogonal;

            Vector3 weaponTangent = directionToEnemyNormalized *
                                    Mathf.Sqrt(_weapon.BulletSpeed * _weapon.BulletSpeed - enemyTangent.sqrMagnitude);

            Vector3 direction = enemyTangent + weaponTangent;

            direction.y = 0;

            _weaponRotationSystem.SetBaseLookDirection(direction);
        }
    }
}