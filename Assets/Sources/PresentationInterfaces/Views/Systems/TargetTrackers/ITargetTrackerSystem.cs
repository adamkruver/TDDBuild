using Sources.PresentationInterfaces.Views.Enemies;

namespace Sources.PresentationInterfaces.Views.Systems.TargetTrackers
{
    public interface ITargetTrackerSystem
    {
        IEnemyView Track(float radius);
    }
}