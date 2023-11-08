using Sources.Controllers.HealthPoints;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.HealthPoints;

namespace Sources.Infrastructure.Factories.Controllers.HealthPoints
{
    public class HealthPresenterFactory
    {
        public HealthPresenter Create(IHealthView view, Health health) =>
            new HealthPresenter(view, health);
    }
}