using Sources.Controllers.Zombies;
using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Systems.Progresses;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.Repositories;
using Sources.Presentation.Views.Zombies;

namespace Sources.Infrastructure.Factories.Controllers.Zombies
{
    public class ZombiePresenterFactory
    {
        private readonly ProgressSystem _progressSystem;
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly EnemyRepository _enemyRepository;
        private readonly EnemyAssessor _enemyAssessor;

        public ZombiePresenterFactory(
            ProgressSystem progressSystem,
            AggressiveSystem aggressiveSystem,
            EnemyRepository enemyRepository,
            EnemyAssessor enemyAssessor
        )
        {
            _progressSystem = progressSystem;
            _aggressiveSystem = aggressiveSystem;
            _enemyRepository = enemyRepository;
            _enemyAssessor = enemyAssessor;
        }

        public ZombiePresenter Create(ZombieView view, Zombie zombie) =>
            new ZombiePresenter(view, zombie, _progressSystem, _aggressiveSystem, _enemyRepository, _enemyAssessor);
    }
}