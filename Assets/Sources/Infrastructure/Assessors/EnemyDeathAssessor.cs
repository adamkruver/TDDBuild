using System;
using System.Collections.Generic;
using Sources.Domain.Enemies;

namespace Sources.Infrastructure.Assessors
{
    public class EnemyDeathAssessor
    {
        private readonly Dictionary<Type, int> _assessors;

        public EnemyDeathAssessor(Dictionary<Type, int> assessors) =>
            _assessors = assessors;

        public int Access(IEnemy enemy)
        {
            Type type = enemy.GetType();

            if (_assessors.ContainsKey(type) == false)
                throw new KeyNotFoundException(nameof(enemy));

            return _assessors[type];
        }
    }
}