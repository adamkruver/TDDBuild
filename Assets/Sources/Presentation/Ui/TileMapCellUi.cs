using UnityEngine;

namespace Sources.Presentation.Ui
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

        public void Show(Vector3 position)
        {
            gameObject.SetActive(true);
            
            Vector3 screenPosition = _camera.WorldToScreenPoint(position);
            Rect rect = _rectTransform.rect;
            float width = rect.width;
            float height = rect.height;

            float x = screenPosition.x / Screen.width * width;
            float y = screenPosition.y / Screen.height * height;

            _rectPosition = new Vector2(x - width / 2, y + height / 12);
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