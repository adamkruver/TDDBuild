using Sources.Controllers;
using Sources.Domain.Weapons;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.InfrastructureInterfaces.Factories.Controllers
{
    public interface IWeaponStateMachineFactory
    {
        IPresenter Create(IWeaponView view, IWeapon weapon, ITargetTrackerSystem targetTrackerSystem);
    }
}