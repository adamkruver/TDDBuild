using System.Threading;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Systems.PathDraw
{
    public interface IPathDrawSystemPointView
    {
        Vector3 NativeScale { get; }
        AnimationCurve ScaleCurve { get; }
        AnimationCurve YPositionCurve { get; }

        void Show(CancellationToken cancellationToken);

        void Show();

        void SetPosition(Vector3 position);

        void SetLocalScale(Vector3 scale);
        void Destroy();
    }
}