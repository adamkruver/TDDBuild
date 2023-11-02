using System;
using System.Collections.Generic;
using Sources.Controllers.Constructs;
using Sources.Domain.Constructs;
using Sources.Domain.Turrets;
using Sources.Domain.Walls;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Controllers.Bullets;
using Sources.Infrastructure.Factories.Controllers.Turrets;
using Sources.Infrastructure.Factories.Controllers.Walls;
using Sources.Infrastructure.Factories.Controllers.Weapons;
using Sources.Infrastructure.Factories.Domain.Turrets;
using Sources.Infrastructure.Factories.Domain.Walls;
using Sources.Infrastructure.Factories.Domain.Weapons;
using Sources.Infrastructure.Factories.Handlers;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Factories.Services;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services.Payments;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.InfrastructureInterfaces.Services.Pointers;
using Sources.InfrastructureInterfaces.Services.Times;
using Sources.Presentation.Views.Cameras;
using Sources.PresentationInterfaces.Views.Constructions;
using Sources.PresentationInterfaces.Views.Constructs;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Controllers.Constructs
{
    public class ConstructButtonPresenterFactory
    {
        private readonly PaymentService _paymentService;
        private readonly TurretViewFactory _turretViewFactory;
        private readonly ConstructService _constructService;
        private readonly Dictionary<string, Func<IConstructionView>> _constructViews;

        public ConstructButtonPresenterFactory(
            ITimeService timeService,
            TurretConstructionViewFactory turretConstructionViewFactory,
            TilemapUntouchablePointerHandlerFactory tilemapUntouchablePointerHandlerFactory,
            TileRepository tileRepository,
            PaymentService paymentService,
            IPointerService pointerService,
            TilemapService tilemapService,
            GameplayCamera gameplayCamera,
            Dictionary<Type, string> weapons
        )
        {
            _paymentService = paymentService;
            

            WallFactory wallFactory = new WallFactory(tileRepository);
            LaserGunFactory laserGunFactory = new LaserGunFactory(timeService);
            RocketGunFactory rocketGunFactory = new RocketGunFactory(timeService);







            WallPresenterFactory wallPresenterFactory = new WallPresenterFactory();
            WallViewFactory wallViewFactory = new WallViewFactory(wallPresenterFactory);

            _constructService = new ConstructService(
                pointerService,
                paymentService,
                tilemapService,
                gameplayCamera,
                tilemapUntouchablePointerHandlerFactory
            );

            _constructService.Disable();
            
            _constructViews = new Dictionary<string, Func<IConstructionView>>()
            {
                [nameof(LaserGun)] = () => turretConstructionViewFactory.Create(new Turret(laserGunFactory.Create())),

/*                [nameof(RocketGun)] = position => turretViewFactory
                    .Create(turretFactory.Create(rocketGunFactory.Create(rocketGunFab), position), position),

                [nameof(MiniGun)] = position => turretViewFactory
                    .Create(turretFactory.Create(rocketGunFactory.Create(rocketGunFab), position), position),

                [nameof(Wall)] = position => wallViewFactory
                    .Create(wallFactory.Create(position), position),*/
            };
        }

        public ConstructButtonPresenter Create(IConstructButtonUi ui, ConstructButton constructButton) =>
            new ConstructButtonPresenter(
                ui,
                constructButton,
                _paymentService,
                _constructService,
                _constructViews[constructButton.Type]
            );
    }
}