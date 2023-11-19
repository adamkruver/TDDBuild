using Sources.Domain.Projectiles;

namespace Sources.Domain.Weapons.Bullets
{
    public interface IBulletWeapon : IWeapon
    {
        public Bullet[] Bullets { get; }

        void SetBullets(Bullet[] bullets);
    }
}