using Sources.Domain.Enemies;
using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.Infrastructure.Repositories;
using Sources.Presentation.Views.Zombies;
using UnityEngine;

namespace Sources.Controllers.Zombies.StateMachines.States
{
    public class DeathState : FiniteStateBase
    {
        private readonly ZombieView _zombieView;
        private readonly Zombie _zombie;
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly EnemyRepository _enemyRepository;
        private readonly EnemyDeathAssessor _enemyDeathAssessor;

        public DeathState(
            ZombieView zombieView,
            Zombie zombie,
            AggressiveSystem aggressiveSystem,
            EnemyRepository enemyRepository,
            EnemyDeathAssessor enemyDeathAssessor
        )
        {
            _zombieView = zombieView;
            _zombie = zombie;
            _aggressiveSystem = aggressiveSystem;
            _enemyRepository = enemyRepository;
            _enemyDeathAssessor = enemyDeathAssessor;
        }

        protected override void OnEnter()
        {
            _aggressiveSystem.AddProgress(_enemyDeathAssessor.Access(_zombie));
            _enemyRepository.Remove(_zombie);
            _zombieView.Die();
        }
    }
}