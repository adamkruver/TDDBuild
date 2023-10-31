using Sources.Domain.Constructs;

namespace Sources.Domain.Weapons
{
    public class LaserGun : IWeapon, IConstructable
    {
        public float BulletSpeed { get; } = 10000000;
        public float FireDistance { get; } = 20;
    }
}