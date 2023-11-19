using Sources.Domain.HealthPoints;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Bullets
{
    public interface ILaserView
    {
        float Duration { get; }
        Vector3 Position { get; }
        Vector3 Forward { get; }
        AudioClip AudioClip { get;}
        float NativeWidth { get; }
        AnimationCurve WidthCurve { get; }

        void SetLaserPositions(Vector3 from, Vector3 to);
        void SetLaserWidth(float width);
        void StartDistortion(Vector3 from, Vector3 to);
        void FinishDistortion();
        void StartBurnTarget(Vector3 position, Vector3 direction);
        void FinishBurnTarget();
        
        (Vector3 position, IDamageable target) Raycast(Ray ray, float maxDistance);
    }
}