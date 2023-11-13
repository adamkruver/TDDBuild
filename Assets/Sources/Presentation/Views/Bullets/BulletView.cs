using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public class BulletView : BulletViewBase, IBulletView
    {
        [SerializeField] private ParticleSystem _bulletParticleSystem;
        [SerializeField] private float _bulletSpeed = 1f;

        private ParticleSystem.MainModule _bulletMain;

        private float Speed => Presenter?.Speed ?? _bulletSpeed;

        protected override void OnAwake() =>
            _bulletMain = _bulletParticleSystem.main;

        public override void Shoot()
        {
            Setup();
            _bulletParticleSystem.Play();
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out IDamageable target) == false)
                return;

            OnShootTarget(target, Transform.forward);
            _bulletParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private void Setup() =>
            SetSpeed(Speed);

        private void SetSpeed(float speed) =>
            _bulletMain.simulationSpeed = speed;
    }
}