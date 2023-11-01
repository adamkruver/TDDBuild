using Sources.Domain.Bullets;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class RocketGunFactory : IWeaponFactory
    {
        private readonly ITimeService _timeService;

        public RocketGunFactory(ITimeService timeService) => 
            _timeService = timeService;

        public IWeapon Create(WeaponFab weaponFab) =>
            new RocketGun(new Rocket(), _timeService, weaponFab);
    }
}