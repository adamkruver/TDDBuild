using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class LaserGunFactory : IWeaponFactory
    {
        private readonly ITimeService _timeService;

        public LaserGunFactory(ITimeService timeService) =>
            _timeService = timeService;

        public IWeapon Create(WeaponFab weaponFab) =>
            new LaserGun(new Laser(), _timeService, weaponFab);
    }
}