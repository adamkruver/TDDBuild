using Sources.Controllers.Bullets;
using Sources.Domain.Bullets;
using Sources.PresentationInterfaces.Views.Bullets;

namespace Sources.Infrastructure.Factories.Controllers.Bullets
{
    public class RocketPresenterFactory
    {
        public RocketPresenter Create(IBulletView view, Rocket rocket) =>
            new RocketPresenter(view, rocket);
    }
}