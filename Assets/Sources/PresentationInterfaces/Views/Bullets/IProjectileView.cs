using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Bullets
{
    public interface IProjectileView
    {
        Vector3 Position { get; }
        void Shoot();
        void SetParent(Transform parent);
    }
}