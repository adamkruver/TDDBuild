using Sources.Domain.Turrets;
using Sources.Infrastructure.Factories.Controllers.Turrets;
using Sources.Presentation.Views.Turrets;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class TurretViewFactory
    {
        private const string PrefabPath = "Views/Turrets/TurretView";

        private readonly TurretPresenterFactory _turretPresenterFactory;
        private readonly WeaponViewFactory _weaponViewFactory;

        public TurretViewFactory(
            TurretPresenterFactory turretPresenterFactory,
            WeaponViewFactory weaponViewFactory
        )
        {
            _turretPresenterFactory = turretPresenterFactory;
            _weaponViewFactory = weaponViewFactory;
        }

        public TurretView Create(Turret turret, Vector2Int position)
        {
            TurretView turretView = Object.Instantiate(Resources.Load<TurretView>(PrefabPath));
            _turretPresenterFactory.Create(turretView, turret);

            turretView.SetWeapon(_weaponViewFactory.Create(turret.Weapon));
            turretView.SetPosition(position);

            return turretView;
        }
    }
}