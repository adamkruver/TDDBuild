using Sources.Controllers.Weapons;
using Sources.Presentation.Views.Bullets;
using Sources.Presentation.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class WeaponView : PresentationViewBase<WeaponStateMachine>, IWeaponView
    {
        [SerializeField] private WeaponRotationSystem _rotationSystem;
        [SerializeField] private ParticleSystem _particleSystem;

        [field: SerializeField] public TargetTrackerSystem TargetTrackerSystem { get; private set; }
        [field: SerializeField] public BulletView Bullet { get; private set; }

        public IWeaponRotationSystem RotationSystem => _rotationSystem;

        public void SetParent(Transform parent) =>
            Transform.SetParent(parent, true);
        
        public void Fire() =>
            _particleSystem.Play();

        private void Update() =>
            Presenter?.Update(Time.deltaTime);
    }
}