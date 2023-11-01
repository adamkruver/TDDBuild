using Sources.Domain.HealthPoints;

namespace Sources.Domain.Bullets
{
    public class Rocket : IBullet
    {
        public float Damage { get; } = 50;
        public float Speed { get; } = 10;
        
        public void Attack(IDamageable damageable) => 
            damageable.TakeDamage(Damage);
    }
}