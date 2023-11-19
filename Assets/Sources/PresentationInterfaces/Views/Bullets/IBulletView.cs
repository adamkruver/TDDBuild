using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Bullets
{
    public interface IBulletView
    {
        void StartProjectile();
        void FinishProjectile();
        AudioClip ShootAudioClip { get; }
        Vector3 Position { get; }
        void SetSpeed(float speed);
    }
}