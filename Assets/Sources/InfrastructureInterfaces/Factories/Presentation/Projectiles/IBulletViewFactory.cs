using Sources.Domain.Projectiles;
using Sources.PresentationInterfaces.Views.Bullets;

namespace Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles
{
    public interface IBulletViewFactory
    {
        IProjectileView Create(Bullet bullet);
    }
}