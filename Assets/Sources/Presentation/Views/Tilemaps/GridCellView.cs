using UnityEngine;

namespace Sources.Presentation.Views.Tilemaps
{
    public abstract class GridCellView : MonoBehaviour
    {
        protected Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = GetComponent<Transform>();
            OnAwake();
        }

        public void SetPosition(Vector2Int position) =>
            Transform.position = new Vector3(position.x, 0, position.y) * 2 + new Vector3(1, 0, 1);

        protected virtual void OnAwake()
        {
        }
    }
}