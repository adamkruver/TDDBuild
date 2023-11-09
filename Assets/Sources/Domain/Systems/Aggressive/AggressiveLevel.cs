using System;
using UnityEngine;

namespace Sources.Domain.Systems.Aggressive
{
    [Serializable]
    public class AggressiveLevel
    {
        [field: SerializeField] public int UpProgress { get; private set; }
        [field: SerializeField] public float AdditionalHealth { get; private set; }
    }
}