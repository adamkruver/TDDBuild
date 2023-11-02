using Sources.Domain.HealthPoints;

namespace Sources.Domain.Bullets
{
    public class Bullet : IBullet
    {
        public float Damage { get; } = 1;
        public float Speed { get; } = 1;
        
        public void Attack(IDamageable damageable) => 
            damageable.TakeDamage(Damage);
    }
}