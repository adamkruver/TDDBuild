using System;
using UnityEngine;

namespace Sources.Presentation.Views.Tilemaps
{
    public class TilemapView : MonoBehaviour
    {
        private void OnMouseEnter()
        {
            Debug.Log("MouseEnter");
        }

        private void OnMouseExit()
        {
            Debug.Log("MouseExit");
        }
    }
}