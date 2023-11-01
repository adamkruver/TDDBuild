using Sources.Controllers.Weapons;
using Sources.Controllers.Weapons.StateMachines.Lasers.States;
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

            CreateStates(stateMachine, weapon, targetTrackerSystem, service);

            return stateMachine;
        }

        private void CreateStates(
            WeaponStateMachine stateMachine,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            WeaponService service
        )
        {
            RotateToTargetState rotateToTargetState = new RotateToTargetState(weapon, targetTrackerSystem, service);

            stateMachine.SetFirstState(rotateToTargetState);
        }
    }
}