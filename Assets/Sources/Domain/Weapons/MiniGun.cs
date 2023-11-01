using Sources.Domain.Bullets;
using Sources.Domain.Constructs;

namespace Sources.Domain.Weapons
{
    public class MiniGun : IWeapon, IConstructable
    {
        public MiniGun(IBullet bullet) => 
            Bullet = bullet;

        public IBullet Bullet { get; }
        public float FireDistance { get; } = 10;
    }
}