using System;

namespace Sources.Domain.Weapons
{
    public interface IWeapon
    {
        event Action Shooting;
        event Action ShootPointChanged;
        
        float ShootSpeed { get; }
        int ShootPointIndex { get; }
        float Cooldown { get; }
        float MinFireDistance { get; }
        float MaxFireDistance { get; }
        float HorizontalRotationSpeed { get; }
        float VerticalRotationSpeed { get; }
        bool CanShoot { get; }
        void Shoot();
    }
}