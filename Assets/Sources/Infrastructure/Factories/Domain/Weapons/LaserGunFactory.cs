using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class LaserGunFactory : WeaponFactoryBase<LaserGun>
    {
        public LaserGunFactory(ITimeService timeService) : base(timeService)
        {
        }

        public override IWeapon Create() =>
            new LaserGun(new Laser(), TimeService, WeaponFab);
    }
}