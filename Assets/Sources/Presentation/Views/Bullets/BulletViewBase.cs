using Sources.Controllers.Bullets;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public abstract class BulletViewBase : PresentationViewBase<BulletPresenter>, IBulletView
    {
        [field: SerializeField] public AudioClip ShootAudioClip { get; private set; }
        public Vector3 Position => Transform.position;

        public abstract void Shoot(); 

        protected void OnShootTarget(IDamageable damageable, Vector3 forward) =>
            Presenter?.Shoot(damageable, forward);
    }
}