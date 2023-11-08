using System;
using Sources.Domain.Enemies;
using Sources.Domain.HealthPoints;
using Sources.Domain.Systems;

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

        private void OnHealthPointsChanged(float healthPoints)
        {
            if (healthPoints == 0)
                Died?.Invoke();
        }

        public void Dispose() =>
            Health.Points.RemoveListener(OnHealthPointsChanged);
    }
}