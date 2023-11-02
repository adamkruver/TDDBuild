using Sources.Controllers.Constructions;
using Sources.Domain.Turrets;
using Sources.Infrastructure.Factories.Domain.Turrets;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.PresentationInterfaces.Views.Constructions;

namespace Sources.Infrastructure.Factories.Controllers.Constructions
{
    public class TurretConstructionPresenterFactory
    {
        private readonly TilemapService _tilemapService;
        private readonly TurretViewFactory _turretViewFactory;
        private readonly TurretFactory _turretFactory;

        public TurretConstructionPresenterFactory(
            TilemapService tilemapService,
            TurretViewFactory turretViewFactory,
            TurretFactory turretFactory
        )
        {
            _tilemapService = tilemapService;
            _turretViewFactory = turretViewFactory;
            _turretFactory = turretFactory;
        }

        public TurretConstructionPresenter Create(ITurretConstructionView view, Turret turret) =>
            new TurretConstructionPresenter(view, turret, _tilemapService, _turretViewFactory, _turretFactory);
    }
}