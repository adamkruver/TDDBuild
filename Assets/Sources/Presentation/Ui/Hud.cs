﻿using UnityEngine;

namespace Sources.Presentation.Ui
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] public ContainerUi TopCenter { get; private set; }
        [field: SerializeField] public ContainerUi TopRight { get; private set; }
        [field: SerializeField] public ContainerUi Footer { get; private set; }
    }
}