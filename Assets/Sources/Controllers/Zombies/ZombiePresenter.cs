using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.Repositories;
using Sources.Presentation.Views.Zombies;
using Sources.PresentationInterfaces.Views.Zombies;

namespace Sources.Controllers.Zombies
{
    public class ZombiePresenter
    {
        private readonly IZombieView _view;
        private readonly Zombie _zombie;
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly EnemyRepository _enemyRepository;
        private readonly EnemyDeathAssessor _enemyDeathAssessor;

        public ZombiePresenter(
            IZombieView view,
            Zombie zombie,
            AggressiveSystem aggressiveSystem,
            EnemyRepository enemyRepository,
            EnemyDeathAssessor enemyDeathAssessor
        )
        {
            _view = view;
            _zombie = zombie;
            _aggressiveSystem = aggressiveSystem;
            _enemyRepository = enemyRepository;
            _enemyDeathAssessor = enemyDeathAssessor;
        }

        public void Enable() =>
            _zombie.Died += OnDied;

        public void Disable() =>
            _zombie.Died -= OnDied;

        private void OnDied()
        {
            _aggressiveSystem.AddProgress(_enemyDeathAssessor.Access(_zombie));
            _enemyRepository.Remove(_zombie);
            _view.Die();
        }
    }
}