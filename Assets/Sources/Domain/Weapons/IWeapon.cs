using Sources.Domain.Bullets;

namespace Sources.Domain.Weapons
{
    public interface IWeapon
    {
        IBullet Bullet { get; }
        float Cooldown { get; }
        float MinFireDistance { get; }
        float MaxFireDistance { get; }
        float HorizontalRotationSpeed { get; }
        float VerticalRotationSpeed { get; }
        bool CanShoot { get; }
        void Fire();
    }
}