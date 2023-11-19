using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Weapons
{
    public interface IShootPointView
    {
        float Offset { get; }
        Vector3 Position { get; }

        void Shoot();
        void SetProjectile(IProjectileView projectile);
    }
}