using Sources.Domain.Bullets;
using Sources.Domain.Systems.Upgrades;

namespace Sources.Infrastructure.Factories.Domain.Bullets
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