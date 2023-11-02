using System;
using Sources.Domain.Turrets;
using Sources.Infrastructure.Factories.Domain.Turrets;
using Sources.Infrastructure.Factories.Presentation.Views;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.PresentationInterfaces.Views.Constructions;
using UnityEngine;

namespace Sources.Controllers.Constructions
{
    public class TurretConstructionPresenter : PresenterBase
    {
        private readonly ITurretConstructionView _view;
        private readonly Turret _turret;
        private readonly TilemapService _tilemapService;
        private readonly TurretViewFactory _turretViewFactory;
        private readonly TurretFactory _turretFactory;

        public TurretConstructionPresenter(
            ITurretConstructionView view,
            Turret turret,
            TilemapService tilemapService,
            TurretViewFactory turretViewFactory,
            TurretFactory turretFactory
        )
        {
            _view = view;
            _turret = turret;
            _tilemapService = tilemapService;
            _turretViewFactory = turretViewFactory;
            _turretFactory = turretFactory;
        }


        public override void Enable()
        {
            _view.SetAttackRadius(_turret.Weapon.MaxFireDistance);
        }

        public void Build(Vector3 position)
        {
            if(_tilemapService.TryGetTilePosition(position, out Vector2Int tilePosition) == false)
                throw new InvalidOperationException(nameof(Build));
            
            Turret turret = _turretFactory.Create(_turret.Weapon, tilePosition);

            _turretViewFactory.Create(turret, tilePosition);
        }
    }
}