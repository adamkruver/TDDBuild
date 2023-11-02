using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class DoubleLaserGunFactory : WeaponFactoryBase<DoubleLaserGun>
    {
        public DoubleLaserGunFactory(ITimeService timeService) : base(timeService)
        {
        }

        public override IWeapon Create() =>
            new DoubleLaserGun(new Laser(), TimeService, WeaponFab);
    }
}