using Sources.Domain.HealthPoints;

namespace Sources.Domain.Bullets
{
    public interface IBullet
    {
        float Damage { get; }

        float Speed { get; }
        void Attack(IDamageable damageable);
    }
}