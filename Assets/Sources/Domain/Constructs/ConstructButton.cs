using Sources.Extensions.Fabs;
using UnityEngine;

namespace Sources.Domain.Constructs
{
    [CreateAssetMenu(fileName = "ConstructButtonFab", menuName = "Fabs/Construct/Button", order = 1)]
    public class ConstructButton : Fab
    {
        [field: SerializeField] public Sprite IconSprite { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
}