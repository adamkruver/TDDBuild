using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public class BulletView : BulletViewBase, IBulletView
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public override void Shoot() =>
            _particleSystem.Play();

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out IDamageable target) == false)
                return;

            OnShootTarget(target, Transform.forward);
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}