using Sources.Controllers.Weapons;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Services.Weapons;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public class WeaponPresenterFactory
    {
        public WeaponPresenter Create(
            IWeaponView view,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem
        )
        {
            return new WeaponPresenter(
                view, weapon, targetTrackerSystem, new WeaponService(weapon, view.RotationSystem)
            );
        }
    }
}