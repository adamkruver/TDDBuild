using UnityEngine;

namespace Sources.Constants
{
    public class Layers
    {
        public static readonly int GameplayGrid = 1 << LayerMask.NameToLayer("GameplayGrid");
        public static readonly int Damageable = 1 << LayerMask.NameToLayer("Damageable");
        public static readonly int Enemy = 1 << LayerMask.NameToLayer("Enemy");
        public static readonly int Obstacle = 1 << LayerMask.NameToLayer("Obstacle");
    }
}