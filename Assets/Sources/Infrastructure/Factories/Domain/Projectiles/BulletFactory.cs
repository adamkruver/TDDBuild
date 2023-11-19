using Sources.Domain.Projectiles;
using Sources.Domain.Systems.Upgrades;

namespace Sources.Infrastructure.Factories.Domain.Projectiles
{
    public class BulletFactory
    {
        private readonly UpgradeSystem _bulletUpgradeSystem;

        public BulletFactory(UpgradeSystem bulletUpgradeSystem) =>
            _bulletUpgradeSystem = bulletUpgradeSystem;

        public Bullet Create() =>
            new Bullet(_bulletUpgradeSystem);
    }
}