using Sources.Domain.Bullets;
using Sources.Domain.Constructs;

namespace Sources.Domain.Weapons
{
    public class RocketGun : IWeapon, IConstructable
    {
        public RocketGun(IBullet bullet) => 
            Bullet = bullet;

        public IBullet Bullet { get; }
        public float FireDistance { get; } = 20;
    }
}