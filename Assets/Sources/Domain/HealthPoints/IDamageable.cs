using UnityEngine;

namespace Sources.Domain.HealthPoints
{
    public interface IDamageable
    {
        void TakeDamage(float damage, Vector3 direction);
    }
}