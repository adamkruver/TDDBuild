using Sources.Controllers;
using Sources.Presentation.Animations.Weapons;
using Sources.Presentation.Views.Bullets;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class WeaponView : PresentationViewBase<IPresenter>, IWeaponView
    {
        [SerializeField] private WeaponAnimation _weaponAnimation;
        [SerializeField] private ParticleSystem _particleSystem;

        [field: SerializeField] public BulletView Bullet { get; private set; }
        [field: SerializeField] public float GunPointOffset { get; private set; }

        public void Shoot()
        {
            _particleSystem.Play();
            _weaponAnimation.Shoot();
        }
    }
}