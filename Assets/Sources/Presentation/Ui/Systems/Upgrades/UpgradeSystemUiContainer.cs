using UnityEngine;

namespace Sources.Presentation.Ui.Systems.Upgrades
{
    public class UpgradeSystemUiContainer : MonoBehaviour
    {
        [field: SerializeField] public UpgradeSystemUi Laser { get; private set; }
        [field: SerializeField] public UpgradeSystemUi Bullet { get; private set; }
        [field: SerializeField] public UpgradeSystemUi Rocket { get; private set; }
    }
}