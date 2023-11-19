using Sources.Domain.Projectiles;
using Sources.PresentationInterfaces.Views.Bullets;

namespace Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles
{
    public interface ILaserViewFactory
    {
        IProjectileView Create(Laser laser);
    }
}