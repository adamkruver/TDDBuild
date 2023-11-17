using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Systems.PathDraw;
using Sources.PresentationInterfaces.Views.Systems.PathDraw;
using UnityEngine;

namespace Sources.Presentation.Views.Systems.PathDraw
{
    public class PathDrawSystemPointView : PresentationViewBase<PathDrawSystemPointPresenter>, IPathDrawSystemPointView
    {
        [field: SerializeField] public Vector3 NativeScale { get; private set; } = Vector3.one;
        [field: SerializeField] public AnimationCurve ScaleCurve { get; private set; }
        [field: SerializeField] public AnimationCurve YPositionCurve { get; private set; }

        public async UniTask Show(CancellationToken cancellationToken) => 
            await Presenter.Show(cancellationToken);

        protected override void OnAfterDisable() =>
            Hide();

        public void SetPosition(Vector3 position) =>
            Transform.position = position;

        public void SetLocalScale(Vector3 scale) =>
            Transform.localScale = scale;
    }
}