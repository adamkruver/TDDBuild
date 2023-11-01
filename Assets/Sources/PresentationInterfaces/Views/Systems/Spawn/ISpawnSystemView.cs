using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Systems.Spawn
{
    public interface ISpawnSystemView
    {
        Vector3 GetRandomSpawnPosition();
    }
}