using Sources.Extensions.Fabs;
using UnityEngine;

namespace Sources.Domain.Systems.EnemySpawn
{
    [CreateAssetMenu(fileName = "EnemySpawnWaveCollectionFab", menuName = "Fabs/Spawn/EnemySpawnWaveCollection", order = 1)]
    public class EnemySpawnWaveCollectionFab : Fab
    {
        [field: SerializeField] public EnemySpawnWaveFab[] Waves { get; private set; }
    }
}