using Sources.Controllers;
using Sources.Presentation.Animations.Weapons;
using Sources.PresentationInterfaces.Views.Bullets;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class ShootPointView : PresentationViewBase<IPresenter>, IShootPointView
    {
        [SerializeField] private ShootAnimation _shootAnimation;
        private IProjectileView _projectile;

        [field: SerializeField] public float Offset { get; private set; }
        
        public Vector3 Position => Transform.position;
        
        public void Shoot()
        {
            _projectile.Shoot();
            _shootAnimation?.Shoot();
        }

        public void SetProjectile(IProjectileView projectile)
        {
            _projectile = projectile;
            _projectile.SetParent(Transform);
        }
    }
}