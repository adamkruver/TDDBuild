using Sources.Domain.Constructs;

namespace Sources.Domain.Weapons
{
    public class RocketGun : IWeapon, IConstructable
    {
        public float BulletSpeed { get; } = 1;
        public float FireDistance { get; } = 20;
    }
}