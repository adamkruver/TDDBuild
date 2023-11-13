using System;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Infrastructure.Services.Weapons
{
    public class WeaponService : IWeaponService
    {
        private readonly IWeapon _weapon;
        private readonly IWeaponRotationSystem _weaponRotationSystem;

        public WeaponService(IWeapon weapon, IWeaponRotationSystem weaponRotationSystem)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));

            _weaponRotationSystem =
                weaponRotationSystem ?? throw new ArgumentNullException(nameof(weaponRotationSystem));
        }

        public void UpdateLookDirectionWithPredict(
            IEnemyView enemy,
            float rotationSpeed,
            float gunpointXOffset,
            Vector3 shootPoint
        ) =>
            _weaponRotationSystem.UpdateRotationBase(
                GetDirectionToEnemy(enemy, gunpointXOffset, shootPoint), rotationSpeed
            );

        public bool HasLockedTarget(IEnemyView enemyView, float gunpointXOffset, Vector3 shootPoint) =>
            _weaponRotationSystem.HasTargetAtLook(GetDirectionToEnemy(enemyView, gunpointXOffset, shootPoint));

        private Vector3 GetDirectionToEnemy(IEnemyView enemy, float gunpointXOffset, Vector3 shootPoint)
        {
     //       Vector3 shootPointOffset = ;
            
            Vector3 directionToEnemyNormalized = (enemy.Position - shootPoint).normalized;
            Vector3 enemyForward = enemy.Forward * enemy.Speed;

            Vector3 enemyOrthogonal = Vector3.Project(enemyForward, directionToEnemyNormalized);

            Vector3 enemyTangent = enemyForward - enemyOrthogonal;
            float sqrBulletSpeed = _weapon.Bullet.Speed * _weapon.Bullet.Speed;
            Vector3 weaponTangent = directionToEnemyNormalized * Mathf.Sqrt(sqrBulletSpeed - enemyTangent.sqrMagnitude);
            Vector3 direction = enemyTangent + weaponTangent;

            direction.y = 0;

     /*       float angle = CalculateAngleCorrection(
                enemy,
                _weaponRotationSystem.Position,
                shootPoint
            );

            if (angle != 0)
                direction = Quaternion.Euler(new Vector3(0, angle, 0)) * direction;
*/
            return direction;
        }

        private float CalculateAngleCorrection(IEnemyView enemy, Vector3 position, Vector3 shootPoint)
        {
            Vector3 rotationSystemToEnemy = enemy.Position - position;
            Vector3 rotationSystemToShootPoint = shootPoint - position;
            float projection = Vector3.Dot(rotationSystemToShootPoint, enemy.Forward);

            float turnRadians = Mathf.Asin(projection / rotationSystemToShootPoint.magnitude) -
                                Mathf.Asin(projection / rotationSystemToEnemy.magnitude);
            
            float angle = turnRadians * Mathf.Rad2Deg;
            
            Debug.Log(angle);
            return angle;
        }
    }
}