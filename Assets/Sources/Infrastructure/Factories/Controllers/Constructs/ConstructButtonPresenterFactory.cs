﻿using System;
using System.Collections.Generic;
using Sources.Controllers.Constructs;
using Sources.Domain.Constructs;
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
using Sources.PresentationInterfaces.Views.Constructs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Controllers.Constructs
{
    public class ConstructButtonPresenterFactory
    {
        private readonly PaymentService _paymentService;
        private readonly TurretViewFactory _turretViewFactory;
        private readonly ConstructService _constructService;
        private readonly Dictionary<string, Action<Vector2Int>> _constructActions;

        public ConstructButtonPresenterFactory(
            ITimeService timeService,
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
            TurretFactory turretFactory = new TurretFactory(tileRepository);
            WallFactory wallFactory = new WallFactory(tileRepository);
            LaserGunFactory laserGunFactory = new LaserGunFactory(timeService);
            RocketGunFactory rocketGunFactory = new RocketGunFactory(timeService);

            TurretPresenterFactory turretPresenterFactory = new TurretPresenterFactory();
            WeaponStateMachineFactory weaponStateMachineFactory = new WeaponStateMachineFactory();
            BulletPresenterFactory bulletPresenterFactory = new BulletPresenterFactory();


            BulletViewFactory bulletViewFactory = new BulletViewFactory(bulletPresenterFactory);
            WeaponViewFactory weaponViewFactory = new WeaponViewFactory(
                weaponStateMachineFactory, bulletViewFactory, weapons
            );
            TurretViewFactory turretViewFactory = new TurretViewFactory(turretPresenterFactory, weaponViewFactory);

            WallPresenterFactory wallPresenterFactory = new WallPresenterFactory();
            WallViewFactory wallViewFactory = new WallViewFactory(wallPresenterFactory);

            _constructService = new ConstructService(
                pointerService,
                paymentService,
                tilemapService,
                gameplayCamera,
                tilemapUntouchablePointerHandlerFactory.Create()
            );

            _constructService.Disable();
            
            WeaponFab laserGunFab = Resources.Load<WeaponFab>("Fabs/Weapons/LaserGunFab");
            WeaponFab rocketGunFab = Resources.Load<WeaponFab>("Fabs/Weapons/RocketGunFab");

            _constructActions = new Dictionary<string, Action<Vector2Int>>()
            {
                [nameof(LaserGun)] = position => turretViewFactory
                    .Create(turretFactory.Create(laserGunFactory.Create(laserGunFab), position), position),

                [nameof(RocketGun)] = position => turretViewFactory
                    .Create(turretFactory.Create(rocketGunFactory.Create(rocketGunFab), position), position),

                [nameof(MiniGun)] = position => turretViewFactory
                    .Create(turretFactory.Create(rocketGunFactory.Create(rocketGunFab), position), position),

                [nameof(Wall)] = position => wallViewFactory
                    .Create(wallFactory.Create(position), position),
            };
        }

        public ConstructButtonPresenter Create(IConstructButtonUi ui, ConstructButton constructButton) =>
            new ConstructButtonPresenter(
                ui,
                constructButton,
                _paymentService,
                _constructService,
                _constructActions[constructButton.Type]
            );
    }
}