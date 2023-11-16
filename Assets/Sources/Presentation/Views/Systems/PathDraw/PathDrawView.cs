using System.Collections.Generic;
using Sources.Controllers.Systems;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.Views.Systems.PathDraw
{
    public class PathDrawView : PresentationViewBase<PathDrawSystemPresenter>, IPathDrawView
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private Button _drawButton;

        private readonly List<IPathPointView> _pathPointViews = new List<IPathPointView>();

        [field: SerializeField] public float Interval { get; private set; }

        public Vector3 StartPoint => _startPoint.position;
        public Vector3 EndPoint => _endPoint.position;

        public void Append(IPathPointView pathPointView)
        {
            _pathPointViews.Add(pathPointView);
        }

        public void Clear()
        {
            for (var i = 0; i < _pathPointViews.Count; i++)
            {
                IPathPointView pathPointView = _pathPointViews[i];
                pathPointView.Clear();
            }

            _pathPointViews.Clear();
        }

        public async void ShowPoints()
        {
            for (var i = 0; i < _pathPointViews.Count; i++)
            {
                var pointView = _pathPointViews[i];
                await pointView.Show().SuppressCancellationThrow();
            }
        }

        public async void HidePoints()
        {
            foreach (var pointView in _pathPointViews) 
                await pointView.Hide().SuppressCancellationThrow();
        }

        protected override void OnBeforeEnable()
        {
            _drawButton.onClick.AddListener(OnShowButtonClicked);
        }

        protected override void OnAfterDisable()
        {
            _drawButton.onClick.RemoveListener(OnShowButtonClicked);
        }

        private void OnShowButtonClicked()
        {
            Presenter?.Draw();
        }
    }
}