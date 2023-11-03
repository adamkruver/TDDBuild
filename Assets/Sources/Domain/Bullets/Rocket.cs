using Sources.Domain.HealthPoints;
using Sources.Domain.Systems.Upgrades;

namespace Sources.Domain.Bullets
{
    public class Rocket : IBullet
    {
        private readonly UpgradeSystem _rocketUpgradeSystem;

        private const float BaseDamage = 50;

        public Rocket(UpgradeSystem rocketUpgradeSystem) =>
            _rocketUpgradeSystem = rocketUpgradeSystem;

        public float Damage => BaseDamage + _rocketUpgradeSystem.Damage.Value;
        public float Speed { get; } = 10;

        public void Attack(IDamageable damageable) =>
            damageable.TakeDamage(Damage);
    }
}