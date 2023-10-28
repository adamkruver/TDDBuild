using Sources.Domain.Turrets;
using Sources.Domain.Weapons;

namespace Sources.Infrastructure.Factories.Domain.Turrets
{
    public class TurretFactory
    {
        public Turret Create(IWeapon weapon)
        {
            return new Turret(weapon);
        }
    }
}