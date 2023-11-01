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

        public void UpdateLookDirectionWithPredict(IEnemyView enemy, float rotationSpeed) => 
            _weaponRotationSystem.UpdateRotationBase(GetDirectionToEnemy(enemy), rotationSpeed);

        public bool HasLockedTarget(IEnemyView enemyView) => 
            _weaponRotationSystem.HasTargetAtLook(GetDirectionToEnemy(enemyView));

        private Vector3 GetDirectionToEnemy(IEnemyView enemy)
        {
            Vector3 directionToEnemyNormalized = (enemy.Position - _weaponRotationSystem.Position).normalized;
            Vector3 enemyForward = enemy.Forward;

            Vector3 enemyOrthogonal =
                Vector3.Dot(directionToEnemyNormalized, enemyForward) * directionToEnemyNormalized;

            Vector3 enemyTangent = enemyForward - enemyOrthogonal;
            float sqrBulletSpeed = _weapon.Bullet.Speed * _weapon.Bullet.Speed;
            Vector3 weaponTangent = directionToEnemyNormalized * Mathf.Sqrt(sqrBulletSpeed - enemyTangent.sqrMagnitude);
            Vector3 direction = enemyTangent + weaponTangent;

            direction.y = 0;

            return direction;
        }
    }
}