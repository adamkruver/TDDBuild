using Sources.Controllers.Weapons;
using Sources.Presentation.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class WeaponView : PresentationViewBase<WeaponPresenter>, IWeaponView
    {
        [SerializeField] private WeaponRotationSystem _rotationSystem;

        [field: SerializeField] public TargetTrackerSystem TargetTrackerSystem { get; private set; }

        public IWeaponRotationSystem RotationSystem => _rotationSystem;

        public void SetParent(Transform parent) =>
            Transform.SetParent(parent, true);

        private void Update() =>
            Presenter?.Update(Time.deltaTime);
    }
}