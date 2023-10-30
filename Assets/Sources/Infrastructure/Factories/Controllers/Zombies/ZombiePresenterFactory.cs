using Sources.Controllers.Zombies;
using Sources.Domain.Systems.Aggressive;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Assessors;
using Sources.Infrastructure.Repositories;
using Sources.Presentation.Views.Zombies;

namespace Sources.Infrastructure.Factories.Controllers.Zombies
{
    public class ZombiePresenterFactory
    {
        private readonly AggressiveSystem _aggressiveSystem;
        private readonly EnemyRepository _enemyRepository;
        private readonly EnemyDeathAssessor _enemyDeathAssessor;

        public ZombiePresenterFactory(
            AggressiveSystem aggressiveSystem,
            EnemyRepository enemyRepository,
            EnemyDeathAssessor enemyDeathAssessor
        )
        {
            _aggressiveSystem = aggressiveSystem;
            _enemyRepository = enemyRepository;
            _enemyDeathAssessor = enemyDeathAssessor;
        }

        public ZombiePresenter Create(ZombieView view, Zombie zombie) =>
            new ZombiePresenter(view, zombie, _aggressiveSystem, _enemyRepository, _enemyDeathAssessor);
    }
}