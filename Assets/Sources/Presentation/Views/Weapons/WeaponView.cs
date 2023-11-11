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

        [field: SerializeField] public BulletViewBase Bullet { get; private set; }
        [field: SerializeField] public float GunPointOffset { get; private set; }

        public void Shoot()
        {
            Bullet.Shoot();
            _weaponAnimation.Shoot();
        }
    }
}