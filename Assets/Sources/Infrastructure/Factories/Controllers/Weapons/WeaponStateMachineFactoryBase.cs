using Sources.Controllers;
using Sources.Controllers.Weapons;
using Sources.Controllers.Weapons.StateMachines.Lasers.States;
using Sources.Controllers.Weapons.StateMachines.Lasers.Transitions;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Factories.Domain.Weapons;
using Sources.Infrastructure.Services.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public abstract class WeaponStateMachineFactoryBase : IWeaponStateMachineFactory
    {
        public IPresenter Create(
            IWeaponView view,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem
        )
        {
            WeaponStateMachine stateMachine = new WeaponStateMachine();
            IWeaponService service = CreateWeaponService(weapon, view.RotationSystem);
            IFiniteState firstState  = CreateStates(view, weapon, targetTrackerSystem, service);
            
            stateMachine.SetFirstState(firstState);

            return stateMachine;
        }

        protected virtual IWeaponService CreateWeaponService(IWeapon weapon, IWeaponRotationSystem rotationSystem) => 
            new WeaponService(weapon, rotationSystem);

        /// <summary>
        /// Create states for weapon state machine
        /// returns first state
        /// </summary>
        protected abstract IFiniteState CreateStates(
            IWeaponView view,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            IWeaponService service
        );
    }
}