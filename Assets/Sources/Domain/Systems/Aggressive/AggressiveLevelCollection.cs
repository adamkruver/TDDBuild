using System.Collections.Generic;
using Sources.Extensions.Fabs;
using UnityEngine;

namespace Sources.Domain.Systems.Aggressive
{
    [CreateAssetMenu(
        fileName = "AggressiveLevelCollectionFab", menuName = "Fabs/Aggressive/Aggressive Level Collection", order = 1
    )]
    public class AggressiveLevelCollection : Fab
    {
        [field: SerializeField] public List<AggressiveLevel> Levels { get; private set; }
    }
}