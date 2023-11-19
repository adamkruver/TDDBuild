using Sources.Controllers.Projectiles;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public class BulletView : PresentationViewBase<BulletPresenter>, IBulletView, IProjectileView
    {
        [SerializeField] private ParticleSystem _bulletParticleSystem;

        [field: SerializeField] public AudioClip ShootAudioClip { get; private set; }

        private ParticleSystem.MainModule _bulletMain;

        public Vector3 Position => Transform.position;

        private void OnParticleCollision(GameObject other) =>
            Presenter?.Collide(other.GetComponent<IDamageable>);

        public void Shoot() =>
            Presenter?.Shoot();

        public void SetParent(Transform parent) => 
            Transform.SetParent(parent, false);

        public void StartProjectile() =>
            _bulletParticleSystem.Play();

        public void FinishProjectile() =>
            _bulletParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

        public void SetSpeed(float speed) =>
            _bulletMain.simulationSpeed = speed;

        protected override void OnAwake() =>
            _bulletMain = _bulletParticleSystem.main;
    }
}