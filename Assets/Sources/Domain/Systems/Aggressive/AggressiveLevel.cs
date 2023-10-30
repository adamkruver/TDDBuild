using System;
using UnityEngine;

namespace Sources.Domain.Systems.Aggressive
{
    [Serializable]
    public class AggressiveLevel
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public int UpProgress { get; private set; }
        [field: SerializeField] public float EnemySpeed { get; private set; }
    }
}