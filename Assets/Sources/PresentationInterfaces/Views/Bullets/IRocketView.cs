using System.Collections.Generic;
using Sources.Domain.HealthPoints;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Bullets
{
    public interface IRocketView
    {
        Vector3 Position { get; }
        float FlyDuration { get; }
        float FlyHeight { get; }
        AnimationCurve HeightCurve { get; }
        AnimationCurve SpeedCurve { get; }
        AudioClip ExplosionAudioClip { get; set; }
        AudioClip EngineAudioClip { get; set; }

        void SetPosition(Vector3 position);
        void SetDirection(Vector3 direction);
        void Detach();
        void StartFireEngine();
        void FinishFireEngine();
        void Explode();
        
        IEnumerable<IDamageable> GetTargets(float radius);
    }
}