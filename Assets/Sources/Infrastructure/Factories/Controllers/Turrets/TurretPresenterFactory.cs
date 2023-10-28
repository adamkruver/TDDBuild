using Sources.Controllers.Turrets;
using Sources.Domain.Turrets;
using Sources.PresentationInterfaces.Views.Turrets;

namespace Sources.Infrastructure.Factories.Controllers.Turrets
{
    public class TurretPresenterFactory
    {
        public TurretPresenter Create(ITurretView view, Turret turret)
        {
            return new TurretPresenter(view, turret);
        }
    }
}