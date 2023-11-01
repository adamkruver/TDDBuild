using System;
using Sources.Attributes;
using Sources.Domain.Enemies;
using UnityEngine;

namespace Sources.Domain.Systems.EnemySpawn
{
    [Serializable]
    public class EnemySpawnObject
    {
        [field: Header("Delay in second before start spawn this group")]
        [field: SerializeField]
        public float GroupDelay { get; private set; } = 1f;

        [field: Header("Spawn interval in second between each spawn in this group")]
        [field: SerializeField]
        public float SpawnIntervalMin { get; private set; } = .1f;

        [field: SerializeField] public float SpawnIntervalMax { get; private set; } = 1;

        [field: Header("Type of spawn enemy")]
        [field: TypedPopup(typeof(IEnemy))]
        [field: SerializeField]
        public string Type { get; private set; }

        [field: Header("Count of spawn enemies in this group")]
        [field: SerializeField]
        public int Count { get; private set; }
    }
}