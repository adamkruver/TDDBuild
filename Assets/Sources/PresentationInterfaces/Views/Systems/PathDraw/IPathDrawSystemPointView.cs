using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Systems.PathDraw
{
    public interface IPathDrawSystemPointView
    {
        Vector3 NativeScale { get; }
        AnimationCurve ScaleCurve { get; }
        AnimationCurve YPositionCurve { get; }

        UniTask Show(CancellationToken cancellationToken);

        void Show();

        void SetPosition(Vector3 position);

        void SetLocalScale(Vector3 scale);
        void Destroy();
    }
}