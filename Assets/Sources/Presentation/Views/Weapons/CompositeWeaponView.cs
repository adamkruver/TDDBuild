using System.Collections.Generic;
using System.Linq;
using Sources.Controllers;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class CompositeWeaponView : PresentationViewBase<IPresenter>, ICompositeWeaponView
    {
        [SerializeField] private WeaponRotationSystem _rotationSystem;

        [field: SerializeField] public WeaponView[] WeaponViews { get; private set; }

        public IWeaponRotationSystem RotationSystem => _rotationSystem;
        public int BarrelsAmount => WeaponViews.Length;
        public float GunPointOffset => WeaponViews[_currentWeaponId].GunPointOffset;

        public Vector3 HeadPosition => _rotationSystem.HeadPosition;

        private int _currentWeaponId;

        public void Shoot(int weaponId)
        {
            _currentWeaponId = weaponId;
            WeaponViews[weaponId].Shoot();
        }
        
        public Vector3 GetShootPoint(int bulletId) =>
            WeaponViews[_currentWeaponId].Bullet.transform.position;

        public void SetParent(Transform parent) =>
            transform.SetParent(parent, true);

        private void Update() =>
            Presenter?.Update(Time.deltaTime);
    }
}