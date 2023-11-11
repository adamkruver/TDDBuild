using Sources.Domain.HealthPoints;
using Sources.Domain.Systems.Upgrades;
using Sources.Frameworks.LiveDatas;
using UnityEngine;

namespace Sources.Domain.Bullets
{
    public class Laser : IBullet
    {
        private readonly LiveData<float> _damageUpgrade;

        private const float BaseDamage = 5;

        public Laser(UpgradeSystem upgradeSystem) =>
            _damageUpgrade = upgradeSystem.Damage.Value;

        public float Damage => BaseDamage + _damageUpgrade.Value;
        public float Speed { get; } = 100000000;

        public void Attack(IDamageable damageable, Vector3 direction) =>
            damageable.TakeDamage(Damage, direction);
    }
}