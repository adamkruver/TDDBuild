using Sources.Controllers.Weapons.StateMachines.Lasers.States;
using Sources.Controllers.Weapons.StateMachines.Lasers.Transitions;
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
            TrackTargetState trackTargetState =
                new TrackTargetState(weapon, targetTrackerSystem, service, compositeView);
            ShootState shootState = new ShootState(compositeView, weapon, shootsAtOnce: 2);

            ToShootStateTransition toShootStateTransition = new ToShootStateTransition(
                shootState, weapon, targetTrackerSystem, service, compositeView
            );

            CooldownState cooldownState = new CooldownState();

            ToCooldownTransition toCooldownTransition = new ToCooldownTransition(cooldownState, weapon);

            ToTrackTargetTransition toTrackTargetTransition = new ToTrackTargetTransition(trackTargetState, weapon);

            trackTargetState.AddTransition(toShootStateTransition);
            shootState.AddTransition(toCooldownTransition);
            cooldownState.AddTransition(toTrackTargetTransition);

            return trackTargetState;
        }
    }
}