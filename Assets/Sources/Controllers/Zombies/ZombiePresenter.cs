using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Systems.Progresses;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.Repositories;
using Sources.PresentationInterfaces.Views.Zombies;

namespace Sources.Controllers.Zombies
{
    public class ZombiePresenter : PresenterBase
    {
        private readonly IZombieView _view;
        private readonly Zombie _zombie;
        private readonly ProgressSystem _progressSystem;
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly EnemyRepository _enemyRepository;
        private readonly EnemyAssessor _enemyAssessor;

        public ZombiePresenter(
            IZombieView view,
            Zombie zombie,
            ProgressSystem progressSystem,
            AggressiveSystem aggressiveSystem,
            EnemyRepository enemyRepository,
            EnemyAssessor enemyAssessor
        )
        {
            _view = view;
            _zombie = zombie;
            _progressSystem = progressSystem;
            _aggressiveSystem = aggressiveSystem;
            _enemyRepository = enemyRepository;
            _enemyAssessor = enemyAssessor;
        }

        public override void Enable() =>
            _zombie.Died += OnDied;

        public override void Disable() =>
            _zombie.Died -= OnDied;

        private void OnDied()
        {
            _progressSystem.AddProgress(_enemyAssessor.Assess(_zombie));
            _aggressiveSystem.AddProgress(_enemyAssessor.Assess(_zombie));
            _enemyRepository.Remove(_zombie);
            _view.Die();
        }
    }
}