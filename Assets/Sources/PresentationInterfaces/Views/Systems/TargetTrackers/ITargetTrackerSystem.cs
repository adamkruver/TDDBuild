using Sources.PresentationInterfaces.Views.Enemies;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Systems.TargetTrackers
{
    public interface ITargetTrackerSystem
    {
        IEnemyView Track(Vector3 position, float minFireDistance, float maxFireDistance);
        bool CanSeeEnemy(Vector3 position, IEnemyView enemy, float minFireDistance, float maxFireDistance);
    }
}