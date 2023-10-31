using Sources.Controllers.Systems;
using Sources.Domain.HealthPoints;
using Sources.PresentationInterfaces.Views.Systems.Damageable;

namespace Sources.Infrastructure.Factories.Controllers.Systems
{
    public class DamageableSystemPresenterFactory
    {
        public DamageableSystemPresenter Create(IDamageableSystemView systemView, Health health)
        {
            return new DamageableSystemPresenter(systemView, health);
        }
    }
}