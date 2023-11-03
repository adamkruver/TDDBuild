using Sources.Controllers;
using Sources.Domain.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.InfrastructureInterfaces.Factories.Controllers
{
    public interface IWeaponStateMachineFactory
    {
        IPresenter Create(ICompositeWeaponView compositeView, IWeaponView[] weaponViews, IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem);
    }
}