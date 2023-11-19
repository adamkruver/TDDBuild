using System;
using Sources.Domain.HealthPoints;
using Sources.Domain.Systems.Upgrades;
using Sources.Frameworks.LiveDatas;
using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;

namespace Sources.Domain.Projectiles
{
    public class Rocket : IBullet
    {
        private readonly LiveData<float> _damageUpgrade;

        private const float BaseDamage = 50;

        public Rocket(UpgradeSystem upgradeSystem) =>
            _damageUpgrade = upgradeSystem.Damage.Value;

        public event Action EnemyChanged;

        public float Damage => BaseDamage + _damageUpgrade.Value;
        public float Speed { get; } = 2;
        public Vector3 Destination { get; private set; }

        public void Attack(IDamageable damageable, Vector3 direction) =>
            damageable.TakeDamage(Damage, direction);

        public void SetDestination(Vector3 destination)
        {
            Destination = destination;
            EnemyChanged?.Invoke();
        }
    }
}