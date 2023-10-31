using Sources.Controllers.Systems;
using Sources.Domain.HealthPoints;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Presentation.Views.Systems.Damageable;

namespace Sources.Infrastructure.Factories.Presentation.Systems
{
    public class DamageableSystemViewFactory
    {
        private readonly DamageableSystemPresenterFactory _damageableSystemPresenterFactory;

        public DamageableSystemViewFactory(DamageableSystemPresenterFactory damageableSystemPresenterFactory)
        {
            _damageableSystemPresenterFactory = damageableSystemPresenterFactory;
        }

        public DamageableSystemView Create(DamageableSystemView view, Health health)
        {
            DamageableSystemPresenter presenter = _damageableSystemPresenterFactory.Create(view, health);
            view.Construct(presenter);

            return view;
        }
    }
}