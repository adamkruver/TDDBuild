using UnityEngine;

namespace Sources.Presentation.Views.Tilemaps
{
    public class ActiveTilemapCellView : MonoBehaviour
    {
        private Transform _transform;
        private GameObject _gameObject;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _gameObject = gameObject;
        }

        public void Show(Vector3Int position)
        {
            _transform.position = new Vector3(position.x, 0, position.y) * 2 + new Vector3(1, 0, 1);
            _gameObject.SetActive(true);
        }

        public void Hide()
        {
            _gameObject.SetActive(false);
        }
    }
}