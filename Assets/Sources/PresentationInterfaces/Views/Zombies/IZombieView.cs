using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Zombies
{
    public interface IZombieView
    {
        void SetDestination(Vector3 destination);
        void Stop();
        void Hit(float lastHitForwardProjection);
        UniTask Fall(float lastHitForwardProjection);
        void DisablePhysics();
    }
}