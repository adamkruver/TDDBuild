using Sources.PresentationInterfaces.Views.Enemies;

namespace Sources.PresentationInterfaces.Views.Bullets
{
    public interface IRocketView : IBulletView
    {
        void SetTarget(IEnemyView enemy);
    }
}