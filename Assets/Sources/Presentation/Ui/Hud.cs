using UnityEngine;

namespace Sources.Presentation.Ui
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] public ContainerUi Header { get; private set; }
        [field: SerializeField] public ContainerUi Footer { get; private set; }
    }
}