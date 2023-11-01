using System;
using Sources.Controllers.Systems;
using Sources.Domain.Systems.EnemySpawn;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Presentation.Views.Systems.Spawn;

namespace Sources.Infrastructure.Factories.Presentation.Systems
{
    public class SpawnSystemViewFactory
    {
        private readonly SpawnSystemPresenterFactory _spawnSystemPresenterFactory;

        public SpawnSystemViewFactory(SpawnSystemPresenterFactory spawnSystemPresenterFactory)
        {
            _spawnSystemPresenterFactory = spawnSystemPresenterFactory ??
                                           throw new ArgumentNullException(nameof(spawnSystemPresenterFactory));
        }

        public SpawnSystemView Create(SpawnSystemView view, EnemySpawnWaveCollectionFab waveCollection)
        {
            SpawnSystemPresenter presenter = _spawnSystemPresenterFactory.Create(view, waveCollection);
            view.Construct(presenter);

            return view;
        }
    }
}