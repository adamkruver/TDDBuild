using Sources.Controllers.Systems;
using Sources.Domain.Systems.EnemySpawn;
using Sources.Domain.Systems.Spawn;
using Sources.InfrastructureInterfaces.Factories.Presentation.View;
using Sources.PresentationInterfaces.Views.Systems.Spawn;

namespace Sources.Infrastructure.Factories.Controllers.Systems
{
    public class SpawnSystemPresenterFactory
    {
        private readonly IEnemyViewFactory _enemyViewFactory;

        public SpawnSystemPresenterFactory(IEnemyViewFactory enemyViewFactory) =>
            _enemyViewFactory = enemyViewFactory;

        public SpawnSystemPresenter Create(ISpawnSystemView view, SpawnSystem system, EnemySpawnWaveCollectionFab collection) =>
            new SpawnSystemPresenter(view, system, collection, _enemyViewFactory);
    }
}