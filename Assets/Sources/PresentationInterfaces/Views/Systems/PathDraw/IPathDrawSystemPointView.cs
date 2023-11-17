using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Systems.PathDraw
{
    public interface IPathDrawSystemPointView
    {
        Vector3 NativeScale { get; }
        AnimationCurve ScaleCurve { get; }
        AnimationCurve FadeInPositionYCurve { get; }
        AnimationCurve FadeOutPositionYCurve { get; }

        void Show();
        void SetPosition(Vector3 position);
        void SetDirection(Vector3 direction);
        void SetLocalScale(Vector3 scale);
        void Destroy();
    }
}