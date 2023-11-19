using Sources.Domain.Projectiles;

namespace Sources.Domain.Weapons.Lasers
{
    public interface ILaserWeapon : IWeapon
    {
        Laser[] Lasers { get; }
        void SetLasers(Laser[] lasers);
    }
}