using Sources.Domain.Bullets;
using Sources.Domain.Weapons;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class LaserGunFactory : IWeaponFactory
    {
        public IWeapon Create() => 
            new LaserGun(new Laser());
    }
}