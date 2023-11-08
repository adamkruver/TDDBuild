using System;
using Sources.Controllers.Zombies;
using Sources.Controllers.Zombies.StateMachines.States;
using Sources.Controllers.Zombies.StateMachines.Transitions;
using Sources.Domain.Credits;
using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Systems.Progresses;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services.Payments;
using Sources.InfrastructureInterfaces.Assessors;
using Sources.Presentation.Views.Zombies;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Controllers.Zombies
{
    public class ZombieStateMachineFactory
    {
        private readonly ProgressSystem _progressSystem;
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly EnemyRepository _enemyRepository;
        private readonly IEnemyAssessor _enemyDeathAggressiveAssessor;
        private readonly IEnemyAssessor _enemyDeathProgressAssessor;
        private readonly IEnemyAssessor _enemyRewardAccessor;
        private readonly PaymentService _paymentService;

        public ZombieStateMachineFactory(
            ProgressSystem progressSystem,
            AggressiveSystem aggressiveSystem,
            EnemyRepository enemyRepository,
            IEnemyAssessor enemyDeathAggressiveAssessor,
            IEnemyAssessor enemyDeathProgressAssessor,
            IEnemyAssessor enemyRewardAccessor,
            PaymentService paymentService
        )
        {
            _progressSystem = progressSystem ?? throw new ArgumentNullException(nameof(progressSystem));
            _aggressiveSystem = aggressiveSystem ?? throw new ArgumentNullException(nameof(aggressiveSystem));
            _enemyRepository = enemyRepository ?? throw new ArgumentNullException(nameof(enemyRepository));
            _enemyDeathAggressiveAssessor = enemyDeathAggressiveAssessor ??
                                            throw new ArgumentNullException(nameof(enemyDeathAggressiveAssessor));
            _enemyDeathProgressAssessor = enemyDeathProgressAssessor ??
                                          throw new ArgumentNullException(nameof(enemyDeathProgressAssessor));
            _enemyRewardAccessor = enemyRewardAccessor;
            _paymentService = paymentService;
        }

        public ZombieStateMachine Create(
            ZombieView view,
            Zombie zombie,
            BaseView baseView
        )
        {
            ZombieStateMachine stateMachine = new ZombieStateMachine();

            CreateStates(view, zombie, stateMachine, baseView.DoorsPosition);

            return stateMachine;
        }

        private void CreateStates(ZombieView view, Zombie zombie, ZombieStateMachine stateMachine, Vector3 destination)
        {
            MoveState moveState = new MoveState(view, destination);
            DeathState deathState = new DeathState(
                view,
                zombie,
                _progressSystem,
                _aggressiveSystem,
                _enemyRepository,
                _enemyDeathAggressiveAssessor,
                _enemyDeathProgressAssessor,
                _enemyRewardAccessor,
                _paymentService
            );

            ToDeathTransition toDeathTransition = new ToDeathTransition(deathState, zombie.Health);

            moveState.AddTransition(toDeathTransition);

            stateMachine.SetFirstState(moveState);
        }
    }
}