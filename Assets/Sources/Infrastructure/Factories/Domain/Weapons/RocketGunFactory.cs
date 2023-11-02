using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class RocketGunFactory : WeaponFactoryBase<RocketGun>
    {
        public RocketGunFactory(ITimeService timeService) : base(timeService)
        {
        }

        public override IWeapon Create() =>
            new RocketGun(new Rocket(), TimeService, WeaponFab);
    }
}