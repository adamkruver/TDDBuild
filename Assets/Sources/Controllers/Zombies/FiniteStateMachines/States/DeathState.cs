using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Systems.Progresses;
using Sources.Domain.Zombies;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services.Payments;
using Sources.InfrastructureInterfaces.Assessors;
using Sources.Presentation.Views.Systems.Damageable;
using Sources.Presentation.Views.Zombies;

namespace Sources.Controllers.Zombies.FiniteStateMachines.States
{
    public class DeathState : FiniteStateBase
    {
        private readonly ZombieView _zombieView;
        private readonly Zombie _zombie;
        private readonly ProgressSystem _progressSystem;
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly EnemyRepository _enemyRepository;
        private readonly IEnemyAssessor _enemyDeathAggressiveAssessor;
        private readonly IEnemyAssessor _enemyDeathProgressAssessor;
        private readonly DamageableSystemView _damageableSystemView;
        private readonly IEnemyAssessor _enemyRewardAssessor;
        private readonly PaymentService _paymentService;

        public DeathState(
            ZombieView zombieView,
            Zombie zombie,
            ProgressSystem progressSystem,
            AggressiveSystem aggressiveSystem,
            EnemyRepository enemyRepository,
            IEnemyAssessor enemyDeathAggressiveAssessor,
            IEnemyAssessor enemyDeathProgressAssessor,
            IEnemyAssessor enemyRewardAssessor,
            PaymentService paymentService
        )
        {
            _zombieView = zombieView;
            _zombie = zombie;
            _progressSystem = progressSystem;
            _aggressiveSystem = aggressiveSystem;
            _enemyRepository = enemyRepository;
            _enemyDeathAggressiveAssessor = enemyDeathAggressiveAssessor;
            _enemyDeathProgressAssessor = enemyDeathProgressAssessor;
            _enemyRewardAssessor = enemyRewardAssessor;
            _paymentService = paymentService;
        }

        protected override void OnEnter()
        {
            _aggressiveSystem.AddProgress(_enemyDeathAggressiveAssessor.Assess(_zombie));
            _progressSystem.AddProgress(_enemyDeathProgressAssessor.Assess(_zombie));
            _enemyRepository.Remove(_zombie);
            _paymentService.Add(_enemyRewardAssessor.Assess(_zombie));
            _zombieView.Stop();
            _zombieView.DisablePhysics();
            _zombie.Decay();
        }
    }
}