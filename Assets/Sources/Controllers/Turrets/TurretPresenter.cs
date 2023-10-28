using Sources.Domain.Turrets;
using Sources.PresentationInterfaces.Views.Turrets;

namespace Sources.Controllers.Turrets
{
    public class TurretPresenter
    {
        private readonly ITurretView _view;
        private readonly Turret _turret;

        public TurretPresenter(ITurretView view, Turret turret)
        {
            _view = view;
            _turret = turret;
        }
    }
}