using System.Collections.Generic;
using System.Linq;
using Sources.Controllers.Projectiles;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public class RocketView : PresentationViewBase<RocketPresenter>, IRocketView, IProjectileView
    {
        [SerializeField] private ParticleSystem _fireParticleSystem;
        [SerializeField] private ParticleSystem _explosionParticleSystem;

        [field: SerializeField] public float FlyDuration { get; private set; } = 2;
        [field: SerializeField] public float FlyHeight { get; private set; } = 3;
        [field: SerializeField] public AnimationCurve HeightCurve { get; private set; }
        [field: SerializeField] public AnimationCurve SpeedCurve { get; private set; }
        [field: SerializeField] public AudioClip ExplosionAudioClip { get; set; }
        [field: SerializeField] public AudioClip EngineAudioClip { get; set; }

        public void SetPosition(Vector3 position) =>
            Transform.position = position;

        public void SetParent(Transform parent)
        {
            Transform.SetParent(parent, false);
            Transform.localPosition = Vector3.zero;
            Transform.localRotation = Quaternion.identity;
        }

        public void SetDirection(Vector3 direction) =>
            Transform.forward = direction.normalized;

        public Vector3 Position => Transform.position;

        public void Shoot() =>
            Presenter?.Shoot();

        public void Explode()
        {
            _explosionParticleSystem.Play();
            Invoke(nameof(Destroy), .5f);
        }

        public void StartFireEngine() =>
            _fireParticleSystem.Play();

        public void FinishFireEngine() =>
            _fireParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        public void Detach() =>
            Transform.parent = null;

        public IEnumerable<IDamageable> GetTargets(float radius) =>
            Physics.OverlapSphere(Position, radius)
                .Where(collider => collider.GetComponent<IDamageable>() != null)
                .Select(collider => collider.GetComponent<IDamageable>());
    }
}