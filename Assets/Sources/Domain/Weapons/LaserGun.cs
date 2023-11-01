using Sources.Domain.Bullets;
using Sources.Domain.Constructs;

namespace Sources.Domain.Weapons
{
    public class LaserGun : IWeapon, IConstructable
    {
        public LaserGun(IBullet bullet) =>
            Bullet = bullet;

        public IBullet Bullet { get; }
        public float FireDistance { get; } = 20;
    }
}