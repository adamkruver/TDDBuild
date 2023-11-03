using Sources.Controllers;
using Sources.Controllers.Weapons;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Services.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Systems.TargetTrackers;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public abstract class WeaponStateMachineFactoryBase : IWeaponStateMachineFactory
    {
        public IPresenter Create(
            ICompositeWeaponView compositeView,
            IWeaponView[] views,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem
        )
        {
            WeaponStateMachine stateMachine = new WeaponStateMachine();
            IWeaponService service = CreateWeaponService(weapon, compositeView.RotationSystem);
            IFiniteState firstState  = CreateStates(compositeView, views, weapon, targetTrackerSystem, service);
            
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
            ICompositeWeaponView compositeView,
            IWeaponView[] views,
            IWeapon weapon,
            ITargetTrackerSystem targetTrackerSystem,
            IWeaponService service
        );
    }
}