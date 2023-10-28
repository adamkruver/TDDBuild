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

        public void Show(Vector3 position)
        {
            _transform.position = position;
            _gameObject.SetActive(true);
        }

        public void Hide()
        {
            _gameObject.SetActive(false);
        }
    }
}