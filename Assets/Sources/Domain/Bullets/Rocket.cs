using Sources.Domain.HealthPoints;
using Sources.Domain.Systems.Upgrades;
using Sources.Frameworks.LiveDatas;
using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;

namespace Sources.Domain.Bullets
{
    public class Rocket : IBullet
    {
        private readonly LiveData<float> _damageUpgrade;

        private const float BaseDamage = 50;

        public Rocket(UpgradeSystem upgradeSystem) =>
            _damageUpgrade = upgradeSystem.Damage.Value;

        public float Damage => BaseDamage + _damageUpgrade.Value;
        public float Speed { get; } = 2;
        public IEnemyView Enemy { get; set; }

        public void Attack(IDamageable damageable, Vector3 direction) =>
            damageable.TakeDamage(Damage, direction);

        public void SetEnemy(IEnemyView enemy) => 
            Enemy = enemy;
    }
}