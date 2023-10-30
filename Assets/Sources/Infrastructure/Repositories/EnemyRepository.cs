using System.Collections.Generic;
using Sources.Domain.Enemies;

namespace Sources.Infrastructure.Repositories
{
    public class EnemyRepository
    {
        private readonly List<IEnemy> _enemies = new List<IEnemy>();
        
        public void Add(IEnemy enemy) => 
            _enemies.Add(enemy);

        public void Remove(IEnemy enemy) => 
            _enemies.Remove(enemy);

        public IReadOnlyCollection<IEnemy> GetAll() => 
            _enemies.ToArray();
    }
}