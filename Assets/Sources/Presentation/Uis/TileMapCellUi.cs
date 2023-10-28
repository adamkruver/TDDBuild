using UnityEngine;

namespace Sources.Presentation.Uis
{
    public class TileMapCellUi : MonoBehaviour
    {
        [SerializeField] RectTransform _rectTransformParent;
        private Camera _camera;
        private Vector2 _rectPosition;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _camera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Show(Vector3 gridPosition)
        {
            gameObject.SetActive(true);
            
            var screenPosition =
                _camera.WorldToScreenPoint(new Vector3(gridPosition.x, 0, gridPosition.y) * 2 + new Vector3(1, 0, 1));

            float x = screenPosition.x / Screen.width * _rectTransformParent.rect.width;
            float y = screenPosition.y / Screen.height * _rectTransformParent.rect.height;

            _rectPosition = new Vector2(x - _rectTransform.rect.width / 2, y + _rectTransformParent.rect.height / 12);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            _rectTransform.anchoredPosition = _rectPosition;
        }
    }
}