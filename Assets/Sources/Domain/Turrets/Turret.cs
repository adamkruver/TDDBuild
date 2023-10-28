using Sources.Domain.Weapons;

namespace Sources.Domain.Turrets
{
    public class Turret
    {
        public Turret(IWeapon weapon)
        {
            Weapon = weapon;
        }

        public IWeapon Weapon { get; }
    }
}