using System;
using Sources.Domain.Enemies;
using Sources.Domain.HealthPoints;

namespace Sources.Domain.Zombies
{
    public class Zombie : IEnemy
    {
        public Zombie(float maxHealthPoints)
        {
            Health = new Health(maxHealthPoints);
            Health.Points.AddListener(OnHealthPointsChanged);
        }

        public event Action Died;

        public Health Health { get; }
        public bool IsDecaying { get; private set; }

        public void Decay() => 
            IsDecaying = true;

        private void OnHealthPointsChanged(float healthPoints)
        {
            if (healthPoints == 0)
                Died?.Invoke();
        }

        public void Dispose() =>
            Health.Points.RemoveListener(OnHealthPointsChanged);
    }
}