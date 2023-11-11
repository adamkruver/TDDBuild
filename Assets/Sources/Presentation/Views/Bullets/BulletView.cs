using Sources.Controllers.Bullets;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Bullets;
using UnityEngine;

namespace Sources.Presentation.Views.Bullets
{
    public abstract class BulletView : PresentationViewBase<BulletPresenter>, IBulletView
    {
        public abstract void Shoot();

        public abstract void OnShootTarget(IDamageable damageable, Vector3 forward);
    }
    /*     [SerializeField] private ParticleSystem _particleSystem;
 
         public void Shoot()
         {
             _particleSystem.Play();
         }
         
         private void OnParticleCollision(GameObject other)
         {
             if (other.TryGetComponent(out IDamageable damageable))
             {
                 Presenter.Fire(damageable, transform.forward);
                 _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
             }
         }*/
}