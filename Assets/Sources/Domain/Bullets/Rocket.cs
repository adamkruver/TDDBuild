using Sources.Domain.HealthPoints;
using Sources.Domain.Systems.Upgrades;
using Sources.Frameworks.LiveDatas;

namespace Sources.Domain.Bullets
{
    public class Rocket : IBullet
    {
        private readonly LiveData<float> _damageUpgrade;

        private const float BaseDamage = 50;

        public Rocket(UpgradeSystem upgradeSystem) =>
            _damageUpgrade = upgradeSystem.Damage.Value;

        public float Damage => BaseDamage + _damageUpgrade.Value;
        public float Speed { get; } = 10;

        public void Attack(IDamageable damageable) =>
            damageable.TakeDamage(Damage);
    }
}