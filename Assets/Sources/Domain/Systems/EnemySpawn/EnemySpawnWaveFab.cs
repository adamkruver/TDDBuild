using Sources.Extensions.Fabs;
using UnityEngine;

namespace Sources.Domain.Systems.EnemySpawn
{
    [CreateAssetMenu(fileName = "EnemySpawnWaveFab", menuName = "Fabs/Spawn/EnemySpawnWave", order = 1)]
    public class EnemySpawnWaveFab : Fab
    {
        [field: SerializeField] public EnemySpawnCollectionFab[] SpawnCollections { get; private set; }
    }
}