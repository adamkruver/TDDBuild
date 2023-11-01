using Sources.Domain.Bullets;

namespace Sources.Domain.Weapons
{
    public interface IWeapon
    {
        IBullet Bullet { get; }
        float FireDistance { get;}
    }
}