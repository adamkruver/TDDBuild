using Sources.Extensions.Fabs;
using UnityEngine;

namespace Sources.Domain.Weapons
{
    [CreateAssetMenu(fileName = "WeaponFab", menuName = "Fabs/Weapons/WeaponFab", order = 1)]
    public class WeaponFab : Fab
    {
        [field: Header("Cooldown time in seconds")]
        [field: SerializeField]
        public float Cooldown { get; private set; }

        [field: Header("Shooting distance")]
        [field: SerializeField]
        public float MinFireDistance { get; private set; }

        [field: SerializeField] public float MaxFireDistance { get; private set; }

        [field: Header("Rotation Speed in seconds")]
        [field: SerializeField]
        public float HorizontalRotationSpeed { get; private set; }

        [field: SerializeField] public float VerticalRotationSpeed { get; private set; }

        [field: Header("Onetime shoots amount")]
        [field: SerializeField]
        public int ShootAtOnce { get; private set; }
    }
}