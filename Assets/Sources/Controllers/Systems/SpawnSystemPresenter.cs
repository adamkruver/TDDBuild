﻿using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Systems.EnemySpawn;
using Sources.InfrastructureInterfaces.Factories.Presentation.View;
using Sources.PresentationInterfaces.Views.Systems.Spawn;
using Random = UnityEngine.Random;

namespace Sources.Controllers.Systems
{
    public class SpawnSystemPresenter : PresenterBase
    {
        private readonly ISpawnSystemView _view;
        private readonly EnemySpawnWaveCollectionFab _collection;
        private readonly IEnemyViewFactory _enemyViewFactory;

        private CancellationTokenSource _cancellationTokenSource;

        public SpawnSystemPresenter(
            ISpawnSystemView view,
            EnemySpawnWaveCollectionFab collection,
            IEnemyViewFactory enemyViewFactory
        )
        {
            _view = view;
            _collection = collection;
            _enemyViewFactory = enemyViewFactory;
        }

        public override void Enable()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            SpawnWaveCollection(_cancellationTokenSource.Token);
        }

        public override void Disable() => 
            _cancellationTokenSource.Cancel();

        private async UniTask SpawnWaveCollection(CancellationToken cancellationToken)
        {
            Queue<EnemySpawnWaveFab> waves = new Queue<EnemySpawnWaveFab>(_collection.Waves);

            while (waves.TryDequeue(out EnemySpawnWaveFab wave) &&
                   cancellationToken.IsCancellationRequested == false)
            {
                await SpawnWave(cancellationToken, wave);
            }
        }

        private async UniTask SpawnWave(CancellationToken cancellationToken, EnemySpawnWaveFab wave)
        {
            Queue<EnemySpawnCollectionFab> spawnCollections = new Queue<EnemySpawnCollectionFab>(wave.SpawnCollections);

            while (spawnCollections.TryDequeue(out EnemySpawnCollectionFab spawnCollection) &&
                   cancellationToken.IsCancellationRequested == false)
            {
                await SpawnCollection(cancellationToken, spawnCollection);
            }
        }

        private async UniTask SpawnCollection(
            CancellationToken cancellationToken,
            EnemySpawnCollectionFab spawnCollection
        )
        {
            Queue<EnemySpawnObject> spawnObjects = new Queue<EnemySpawnObject>(spawnCollection.SpawnObjects);

            while (spawnObjects.TryDequeue(out EnemySpawnObject spawnObject) &&
                   cancellationToken.IsCancellationRequested == false)
            {
                await SpawnEnemy(cancellationToken, spawnObject);
                await UniTask.Delay(TimeSpan.FromSeconds(spawnObject.GroupDelay), cancellationToken: cancellationToken);
            }

        }

        private async UniTask SpawnEnemy(CancellationToken cancellationToken, EnemySpawnObject spawnObject)
        {
            for (int i = 0; i < spawnObject.Count; i++)
            {
                _enemyViewFactory.Create(spawnObject.Type, _view.GetRandomSpawnPosition());

                float spawnInterval = Random.Range(
                    spawnObject.SpawnIntervalMin, spawnObject.SpawnIntervalMax
                );

                await UniTask.Delay(TimeSpan.FromSeconds(spawnInterval), cancellationToken: cancellationToken);
            }
        }
    }
}