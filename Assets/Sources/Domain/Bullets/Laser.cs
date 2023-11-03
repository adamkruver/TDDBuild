using Sources.Domain.HealthPoints;
using Sources.Domain.Systems.Upgrades;

namespace Sources.Domain.Bullets
{
    public class Laser : IBullet
    {
        private readonly UpgradeSystem _laserUpgradeSystem;

        private const float BaseDamage = 5;
        
        public Laser(UpgradeSystem laserUpgradeSystem) => 
            _laserUpgradeSystem = laserUpgradeSystem;

        public float Damage => BaseDamage + _laserUpgradeSystem.Damage.Value;
        public float Speed { get; } = 100000000000;
        
        public void Attack(IDamageable damageable) => 
            damageable.TakeDamage(Damage);
    }
}