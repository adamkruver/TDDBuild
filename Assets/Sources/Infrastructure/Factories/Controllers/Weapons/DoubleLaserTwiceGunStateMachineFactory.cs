using Sources.Controllers.Weapons.StateMachines.Lasers.States;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public class DoubleLaserTwiceGunStateMachineFactory: WeaponStateMachineFactoryBase
    {
        protected override IFiniteState CreateStates(
            IWeaponView view,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            IWeaponService service
        )
        {
            return new TrackTargetState(weapon, targetTrackerSystem, service);
        }
    }
}