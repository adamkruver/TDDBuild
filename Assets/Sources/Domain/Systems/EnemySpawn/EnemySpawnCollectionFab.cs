using Sources.Extensions.Fabs;
using UnityEngine;

namespace Sources.Domain.Systems.EnemySpawn
{
    [CreateAssetMenu(fileName = "EnemySpawnCollectionFab", menuName = "Fabs/Spawn/EnemySpawnCollection", order = 1)]
    public class EnemySpawnCollectionFab : Fab
    {
        [field: SerializeField] public EnemySpawnObject[] SpawnObjects { get; private set; }
    }
}