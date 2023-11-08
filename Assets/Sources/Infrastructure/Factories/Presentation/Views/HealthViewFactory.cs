using Sources.Controllers.HealthPoints;
using Sources.Domain.HealthPoints;
using Sources.Infrastructure.Factories.Controllers.HealthPoints;
using Sources.Presentation.Views.HealthPoints;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class HealthViewFactory
    {
        private readonly HealthPresenterFactory _healthPresenterFactory;

        public HealthViewFactory(HealthPresenterFactory healthPresenterFactory) => 
            _healthPresenterFactory = healthPresenterFactory;

        public HealthView Create(HealthView view, Health health)
        {
            HealthPresenter presenter = _healthPresenterFactory.Create(view, health);
            view.Construct(presenter);

            return view;
        }
    }
}