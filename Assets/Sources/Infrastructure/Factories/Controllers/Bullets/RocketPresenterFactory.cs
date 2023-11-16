using Sources.Controllers.Bullets;
using Sources.Domain.Bullets;
using Sources.Presentation.Views.Bullets;

namespace Sources.Infrastructure.Factories.Controllers.Bullets
{
    public class RocketPresenterFactory
    {
        public RocketPresenter Create(RocketView view, Rocket rocket) =>
            new RocketPresenter(view, rocket);
    }
}