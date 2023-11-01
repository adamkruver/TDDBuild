using Sources.Controllers.Bullets;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public class BulletView : PresentationViewBase<BulletPresenter>, IBulletView
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                Presenter.Fire(damageable);
        }
    }
}