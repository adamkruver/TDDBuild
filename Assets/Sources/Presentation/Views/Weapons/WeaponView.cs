using Sources.Controllers;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class WeaponView : PresentationViewBase<IPresenter>, IWeaponView
    {
        [SerializeField] private WeaponRotationSystem _rotationSystem;

        [field: SerializeField] public ShootPointView[] ShootPointsView { get; private set; }

        public IShootPointView[] ShootPoints => ShootPointsView;
        public IShootPointView ActiveShootPoint { get; private set; }
        public IWeaponRotationSystem RotationSystem => _rotationSystem;
        public int BarrelsAmount => ShootPointsView.Length;
        public float GunPointOffset => ActiveShootPoint.Offset;
        public Vector3 HeadPosition => _rotationSystem.HeadPosition;

        public void Shoot() =>
            ActiveShootPoint.Shoot();

        public Vector3 GetShootPointPosition() =>
            ActiveShootPoint.Position;

        public void SetActiveShootPoint(int index) =>
            ActiveShootPoint = ShootPointsView[index];

        public void SetParent(Transform parent) =>
            transform.SetParent(parent, true);

        private void Update() =>
            Presenter?.Update(Time.deltaTime);
    }
}