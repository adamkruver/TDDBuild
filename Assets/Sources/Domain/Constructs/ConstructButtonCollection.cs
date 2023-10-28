using Sources.Extensions.Fabs;
using UnityEngine;

namespace Sources.Domain.Constructs
{
    [CreateAssetMenu(
        fileName = "ConstructButtonCollectionFab", menuName = "Fabs/Construct/Button Collection", order = 1
    )]
    public class ConstructButtonCollection : Fab
    {
        [field: SerializeField] public ConstructButton[] ConstructButtons { get; private set; }
    }
}