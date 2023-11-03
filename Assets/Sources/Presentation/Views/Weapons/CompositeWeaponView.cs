using System.Linq;
using Sources.Presentation.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class CompositeWeaponView : MonoBehaviour, ICompositeWeaponView
    {
        [SerializeField] private WeaponView[] _weaponViews;
        [SerializeField] private WeaponRotationSystem _rotationSystem;

        [field: SerializeField] public TargetTrackerSystem TargetTrackerSystem { get; private set; }

        public IWeaponView[] WeaponViews => _weaponViews.Select(weapon => (IWeaponView)weapon).ToArray();
        public IWeaponRotationSystem RotationSystem => _rotationSystem;

        public void SetParent(Transform parent) =>
            transform.SetParent(parent, true);
    }
}