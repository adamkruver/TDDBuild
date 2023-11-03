using System;
using System.Collections.Generic;
using Sources.Domain.Enemies;
using Sources.InfrastructureInterfaces.Assessors;

namespace Sources.Infrastructure.Assessors
{
    public class EnemyAssessor : IEnemyAssessor
    {
        private readonly Dictionary<Type, int> _assessors;

        public EnemyAssessor(Dictionary<Type, int> assessors) =>
            _assessors = assessors;

        public int Assess(IEnemy enemy)
        {
            Type type = enemy.GetType();

            if (_assessors.ContainsKey(type) == false)
                throw new KeyNotFoundException(nameof(enemy));

            return _assessors[type];
        }
    }
}