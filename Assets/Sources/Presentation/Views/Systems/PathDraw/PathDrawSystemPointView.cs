using System.Threading;
using Sources.Controllers.Systems.PathDraw;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Presentation.Views.Systems.PathDraw
{
    public class PathDrawSystemPointView : PresentationViewBase<PathDrawSystemPointPresenter>, IPathDrawSystemPointView
    {
        [field: SerializeField] public Vector3 NativeScale { get; private set; } = Vector3.one;
        [field: SerializeField] public AnimationCurve ScaleCurve { get; private set; }
        [field: SerializeField] public AnimationCurve FadeInPositionYCurve { get; private set; }
        [field: SerializeField] public AnimationCurve FadeOutPositionYCurve { get; private set; }

        public async void ShowAsync(CancellationToken cancellationToken) => 
            await Presenter.ShowAsync(cancellationToken);

        public void SetPosition(Vector3 position) =>
            Transform.position = position;

        public void SetDirection(Vector3 direction) => 
            Transform.forward = direction;

        public void SetLocalScale(Vector3 scale) =>
            Transform.localScale = scale;
    }
}