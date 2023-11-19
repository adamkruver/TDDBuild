using Sources.Domain.Projectiles;
using Sources.Domain.Systems.Upgrades;

namespace Sources.Infrastructure.Factories.Domain.Projectiles
{
    public class LaserFactory
    {
        private readonly UpgradeSystem _laserUpgradeSystem;

        public LaserFactory(UpgradeSystem laserUpgradeSystem) => 
            _laserUpgradeSystem = laserUpgradeSystem;

        public Laser Create() => 
            new Laser(_laserUpgradeSystem);
    }
}