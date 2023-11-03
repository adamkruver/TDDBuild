using Sources.Controllers.Weapons.StateMachines.Lasers.States;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public class RocketTwiceGunStateMachineFactory : WeaponStateMachineFactoryBase
    {
        protected override IFiniteState CreateStates(
            ICompositeWeaponView compositeView,
            IWeaponView[] views,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            IWeaponService service
        )
        {
            return new TrackTargetState(weapon, targetTrackerSystem, service);
        }
    }
}