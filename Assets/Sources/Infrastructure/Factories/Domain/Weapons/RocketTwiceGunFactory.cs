using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class RocketTwiceGunFactory : WeaponFactoryBase<RocketTwiceGun>
    {
        public RocketTwiceGunFactory(ITimeService timeService) : base(timeService)
        {
        }

        public override IWeapon Create() =>
            new RocketTwiceGun(new Rocket(), TimeService, WeaponFab);
    }
}