using Sources.Controllers.Systems;
using Sources.Domain.Systems.Aggressive;
using Sources.Infrastructure.Repositories;
using Sources.PresentationInterfaces.Ui.Systems;

namespace Sources.Infrastructure.Factories.Controllers.Systems
{
    public class AggressiveSystemPresenterFactory
    {
        private readonly EnemyRepository _enemyRepository;

        public AggressiveSystemPresenterFactory(EnemyRepository enemyRepository) =>
            _enemyRepository = enemyRepository;

        public AggressiveSystemPresenter Create(IAggressiveSystemUi ui, AggressiveSystem system) =>
            new AggressiveSystemPresenter(ui, system, _enemyRepository);
    }
}