using Sources.Controllers.Systems.PathDraw;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.Presentation.Views.Systems.PathDraw
{
    public class PathDrawSystemView : PresentationViewBase<PathDrawSystemPresenter>, IPathDrawSystemView
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private Button _drawButton;

        [field: SerializeField] public float Distance { get; private set; }
        [field: SerializeField] public float SpawnInterval { get; private set; }

        public Vector3 StartPoint => _startPoint.position;
        public Vector3 EndPoint => _endPoint.position;

        public void AddButtonListener(UnityAction action) => 
            _drawButton.onClick.AddListener(action);

        public void RemoveButtonListener(UnityAction action) => 
            _drawButton.onClick.RemoveListener(action);
    }
}