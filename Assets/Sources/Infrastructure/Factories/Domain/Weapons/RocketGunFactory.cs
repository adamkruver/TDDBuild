using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class RocketGunFactory : WeaponFactoryBase<RocketTwiceGun>
    {
        public RocketGunFactory(ITimeService timeService) : base(timeService)
        {
        }

        public override IWeapon Create() =>
            new RocketTwiceGun(new Rocket(), TimeService, WeaponFab);
    }
}