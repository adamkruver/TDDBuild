using Sources.Domain.Bullets;
using Sources.Domain.Weapons;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public class RocketGunFactory : IWeaponFactory
    {
        public IWeapon Create() => 
            new RocketGun(new Rocket());
    }
}