using Sources.Domain.HealthPoints;
using Sources.Domain.Systems.Upgrades;
using Sources.Frameworks.LiveDatas;

namespace Sources.Domain.Bullets
{
    public class Laser : IBullet
    {
        private readonly LiveData<float> _damageUpgrade;

        private const float BaseDamage = 5;

        public Laser(UpgradeSystem upgradeSystem) =>
            _damageUpgrade = upgradeSystem.Damage.Value;

        public float Damage => BaseDamage + _damageUpgrade.Value;
        public float Speed { get; } = 100;

        public void Attack(IDamageable damageable) =>
            damageable.TakeDamage(Damage);
    }
}