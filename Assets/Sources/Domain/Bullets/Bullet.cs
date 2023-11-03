using Sources.Domain.HealthPoints;
using Sources.Domain.Systems.Upgrades;

namespace Sources.Domain.Bullets
{
    public class Bullet : IBullet
    {
        private readonly UpgradeSystem _bulletUpgradeSystem;

        private const float BaseDamage = 1;

        public Bullet(UpgradeSystem bulletUpgradeSystem) =>
            _bulletUpgradeSystem = bulletUpgradeSystem;

        public float Damage => BaseDamage + _bulletUpgradeSystem.Damage.Value;
        public float Speed { get; } = 1;

        public void Attack(IDamageable damageable) =>
            damageable.TakeDamage(Damage);
    }
}