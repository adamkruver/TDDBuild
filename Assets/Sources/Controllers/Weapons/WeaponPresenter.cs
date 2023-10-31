using System;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Services.Weapons;
using Sources.PresentationInterfaces.Views.Enemies;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Controllers.Weapons
{
    public class WeaponPresenter : PresenterBase
    {
        private readonly IWeaponView _view;
        private readonly IWeapon _weapon;
        private readonly ITargetTrackerSystem _targetTrackerSystem;
        private readonly WeaponService _weaponService;

        public WeaponPresenter(
            IWeaponView view,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            WeaponService weaponService
        )
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _targetTrackerSystem = targetTrackerSystem ?? throw new ArgumentNullException(nameof(targetTrackerSystem));
            _weaponService = weaponService ?? throw new ArgumentNullException(nameof(weaponService));
        }

        public void Update(float deltaTime)
        {
            IEnemyView enemyView = _targetTrackerSystem.Track(_weapon.FireDistance);
            _weaponService.LookWithPredict(enemyView);
        }
    }
}