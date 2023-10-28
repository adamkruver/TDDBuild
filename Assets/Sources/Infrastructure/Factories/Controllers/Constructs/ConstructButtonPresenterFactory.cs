using System;
using System.Collections.Generic;
using Sources.Controllers.Constructs;
using Sources.Domain.Constructs;
using Sources.Infrastructure.Factories.Controllers.Turrets;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Listeners.Pointers;
using Sources.Infrastructure.Listeners.Pointers.Untouchable;
using Sources.InfrastructureInterfaces.Services.Pointers;
using Sources.Presentation.Views.Cameras;
using Sources.PresentationInterfaces.Views.Constructs;

namespace Sources.Infrastructure.Factories.Controllers.Constructs
{
    public class ConstructButtonPresenterFactory
    {
        private readonly IPointerService _pointerService;
        private readonly TurretViewFactory _turretViewFactory;
        private readonly TilemapUntouchablePointerHandler _constructUntouchablePointerHandler;
        private readonly GameplayInteractPointerHandler _constructPointerHandler;

        public ConstructButtonPresenterFactory(
            IPointerService pointerService,
            GameplayCamera gameplayCamera,
            Dictionary<Type, string> weapons
        )
        {
            _pointerService = pointerService;

            TurretPresenterFactory turretPresenterFactory = new TurretPresenterFactory();
            WeaponPresenterFactory weaponPresenterFactory = new WeaponPresenterFactory();

            WeaponViewFactory weaponViewFactory = new WeaponViewFactory(weaponPresenterFactory, weapons);
            TurretViewFactory turretViewFactory = new TurretViewFactory(turretPresenterFactory, weaponViewFactory);

            _constructUntouchablePointerHandler = new TilemapUntouchablePointerHandler(gameplayCamera);
            _constructPointerHandler = new GameplayInteractPointerHandler(turretViewFactory, gameplayCamera);
        }

        public ConstructButtonPresenter Create(IConstructButtonUi ui, ConstructButton constructButton) =>
            new ConstructButtonPresenter(
                ui,
                constructButton,
                _pointerService,
                _constructPointerHandler,
                _constructUntouchablePointerHandler
            );
    }
}