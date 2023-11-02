using Sources.Domain.HealthPoints;

namespace Sources.Domain.Bullets
{
    public class Laser : IBullet
    {
        public float Damage { get; } = 5;
        public float Speed { get; } = 100000;
        
        public void Attack(IDamageable damageable) => 
            damageable.TakeDamage(Damage);
    }
}