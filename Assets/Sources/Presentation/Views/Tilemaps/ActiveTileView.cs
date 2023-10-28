using Sources.Controllers.Tilemaps;
using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Presentation.Views.Tilemaps
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ActiveTileView : MonoBehaviour, IActiveView
    {
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        
        private Transform _transform;
        private GameObject _gameObject;
        private ActiveTilePresenter _activeTilePresenter;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _gameObject = gameObject;
        }

        public void Construct(ActiveTilePresenter activeTilePresenter) =>
            _activeTilePresenter = activeTilePresenter;

        public void Show(Vector3 position)
        {
            _transform.position = position;
            _gameObject.SetActive(true);
            _activeTilePresenter?.OnChangedPosition(position);
        }

        public void Hide() => 
            _gameObject.SetActive(false);

        public void Activate() => 
            _spriteRenderer.color = _activeColor;

        public void Deactivate() => 
            _spriteRenderer.color = _inactiveColor;
    }
}