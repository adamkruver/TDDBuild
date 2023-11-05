using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Zombies
{
    public interface IZombieView
    {
        void SetDestination(Vector3 destination);
        void Stop();
        void Die();
    }
}