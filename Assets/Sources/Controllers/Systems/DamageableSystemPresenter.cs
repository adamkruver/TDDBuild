using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Systems.Damageable;
using UnityEngine;

namespace Sources.Controllers.Systems
{
    public class DamageableSystemPresenter : PresenterBase, IDamageable
    {
        private readonly IDamageableSystemView _damageableSystemView;
        private readonly Health _health;

        public DamageableSystemPresenter(IDamageableSystemView damageableSystemView, Health health)
        {
            _damageableSystemView = damageableSystemView;
            _health = health;
        }

        public override void Enable()
        {
            _health.Points.AddListener(OnHealthPointsChanged);
            _health.NormalizedPoints.AddListener(OnHealthPointsNormalizedChanged);
        }

        public override void Disable()
        {
            _health.Points.RemoveListener(OnHealthPointsChanged);
            _health.NormalizedPoints.RemoveListener(OnHealthPointsNormalizedChanged);
        }

        public void TakeDamage(float damage, Vector3 direction) =>
            _health.TakeDamage(damage, direction);

        private void OnHealthPointsChanged(float healthPoints)
        {
        }

        private void OnHealthPointsNormalizedChanged(float normalizedHealthPoints)
        {
        }
    }
}