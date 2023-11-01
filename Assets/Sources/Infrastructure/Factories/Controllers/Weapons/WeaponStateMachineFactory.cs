using Sources.Controllers.Weapons;
using Sources.Controllers.Weapons.StateMachines.Lasers.States;
using Sources.Controllers.Weapons.StateMachines.Lasers.Transitions;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Services.Weapons;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public class WeaponStateMachineFactory
    {
        public WeaponStateMachine Create(
            IWeaponView view,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem
        )
        {
            WeaponStateMachine stateMachine = new WeaponStateMachine();
            WeaponService service = new WeaponService(weapon, view.RotationSystem);

            CreateStates(view, stateMachine, weapon, targetTrackerSystem, service);

            return stateMachine;
        }

        private void CreateStates(
            IWeaponView view,
            WeaponStateMachine stateMachine,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            WeaponService service
        )
        {
            TrackTargetState trackTargetState = new TrackTargetState(weapon, targetTrackerSystem, service);
            ShootState shootState = new ShootState(view, view.Animation, weapon);

            ToShootStateTransition toShootStateTransition = new ToShootStateTransition(
                shootState, weapon, targetTrackerSystem, service
            );

            CooldownState cooldownState = new CooldownState();

            ToCooldownTransition toCooldownTransition = new ToCooldownTransition(cooldownState, weapon);

            ToTrackTargetTransition toTrackTargetTransition = new ToTrackTargetTransition(trackTargetState, weapon);

            trackTargetState.AddTransition(toShootStateTransition);
            shootState.AddTransition(toCooldownTransition);
            cooldownState.AddTransition(toTrackTargetTransition);

            stateMachine.SetFirstState(trackTargetState);
        }
    }
}