using System;
using Sources.Controllers.Systems;
using Sources.Domain.Systems.EnemySpawn;
using Sources.Domain.Systems.Spawn;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Presentation.Ui.Systems.Spawn;
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

        public SpawnSystemView Create(SpawnSystemView view, SpawnSystem system, EnemySpawnWaveCollectionFab waveCollection, SpawnNotifierUi spawnNotifierUi)
        {
            SpawnSystemPresenter presenter = _spawnSystemPresenterFactory.Create(view, system, waveCollection);
            view.Construct(presenter);
            view.AddNotifierUi(spawnNotifierUi);

            return view;
        }
    }
}