using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class MiniTwiceGunFactory : WeaponFactoryBase<MiniTwiceGun>
    {
        public MiniTwiceGunFactory(ITimeService timeService) : base(timeService)
        {
        }

        public override IWeapon Create() =>
            new MiniTwiceGun(new Bullet(), TimeService, WeaponFab);
    }
}