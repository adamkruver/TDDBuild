using Sources.Controllers.Systems;
using Sources.Domain.Systems.Aggressive;
using Sources.Infrastructure.Repositories;
using Sources.PresentationInterfaces.Views.Systems.Aggressive;

namespace Sources.Infrastructure.Factories.Controllers.Systems
{
    public class AggressiveSystemPresenterFactory
    {
        private readonly EnemyRepository _enemyRepository;

        public AggressiveSystemPresenterFactory(EnemyRepository enemyRepository) =>
            _enemyRepository = enemyRepository;

        public AggressiveSystemPresenter Create(IAggressiveSystemView view, AggressiveSystem system) =>
            new AggressiveSystemPresenter(view, system, _enemyRepository);
    }
}