using Sources.Controllers.Zombies;
using Sources.Controllers.Zombies.StateMachines.States;
using Sources.Controllers.Zombies.StateMachines.Transitions;
using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Repositories;
using Sources.Presentation.Views.Zombies;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Controllers.Zombies
{
    public class ZombieStateMachineFactory
    {
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly EnemyRepository _enemyRepository;
        private readonly EnemyDeathAssessor _enemyDeathAssessor;

        public ZombieStateMachineFactory(
            AggressiveSystem aggressiveSystem,
            EnemyRepository enemyRepository,
            EnemyDeathAssessor enemyDeathAssessor
        )
        {
            _aggressiveSystem = aggressiveSystem;
            _enemyRepository = enemyRepository;
            _enemyDeathAssessor = enemyDeathAssessor;
        }

        public ZombieStateMachine Create(ZombieView view,
            Zombie zombie, BaseView baseView)
        {
            ZombieStateMachine stateMachine = new ZombieStateMachine();

            CreateStates(view, zombie, stateMachine, baseView.DoorsPosition);

            return stateMachine;
        }

        private void CreateStates(ZombieView view, Zombie zombie, ZombieStateMachine stateMachine, Vector3 destination)
        {
            MoveState moveState = new MoveState(view, destination);
            DeathState deathState = new DeathState(view, zombie, _aggressiveSystem, _enemyRepository, _enemyDeathAssessor);

            ToDeathTransition toDeathTransition = new ToDeathTransition(deathState, zombie);

            moveState.AddTransition(toDeathTransition);

            stateMachine.SetFirstState(moveState);
        }
    }
}