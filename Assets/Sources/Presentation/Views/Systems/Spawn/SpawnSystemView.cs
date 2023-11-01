using System.IO;
using Sources.Controllers.Systems;
using Sources.PresentationInterfaces.Views.Systems.Spawn;
using UnityEngine;

namespace Sources.Presentation.Views.Systems.Spawn
{
    public class SpawnSystemView : PresentationViewBase<SpawnSystemPresenter>, ISpawnSystemView
    {
        [SerializeField] Transform[] _spawnPositions;

        protected override void OnAwake()
        {
            if (_spawnPositions.Length == 0)
                throw new InvalidDataException(nameof(_spawnPositions));
        }

        public Vector3 GetRandomSpawnPosition() =>
            _spawnPositions[Random.Range(0, _spawnPositions.Length)].position;
    }
}