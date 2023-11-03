using Sources.Domain.Bullets;
using Sources.Domain.Systems.Upgrades;

namespace Sources.Infrastructure.Factories.Domain.Bullets
{
    public class RocketFactory
    {
        private readonly UpgradeSystem _rocketUpgradeSystem;

        public RocketFactory(UpgradeSystem rocketUpgradeSystem) => 
            _rocketUpgradeSystem = rocketUpgradeSystem;

        public Rocket Create() => 
            new Rocket(_rocketUpgradeSystem);
    }
}