using Sources.Domain.Constructs;

namespace Sources.Domain.Weapons
{
    public class MiniGun : IWeapon, IConstructable
    {
        public float BulletSpeed { get; } = 2;
        public float FireDistance { get; } = 10;
    }
}