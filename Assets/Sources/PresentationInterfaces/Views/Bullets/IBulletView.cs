using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Bullets
{
    public interface IBulletView
    {
        void Shoot();
        AudioClip ShootAudioClip { get; }
        Vector3 Position { get; }
    }
}