using Sources.Controllers;
using Sources.Controllers.Weapons;
using Sources.Domain.Weapons;
using Sources.Infrastructure.Services.Weapons;
using Sources.InfrastructureInterfaces.Factories.Controllers;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public abstract class WeaponStateMachineFactoryBase : IWeaponStateMachineFactory
    {
        public IPresenter Create(
            IWeaponView view,
            IWeapon weapon,
            ITargetProvider targetProvider
        )
        {
            WeaponStateMachine stateMachine = new WeaponStateMachine(weapon, view);
            IWeaponService service = CreateWeaponService(weapon, view.RotationSystem);
            IFiniteState firstState = CreateStates(view, weapon, service, targetProvider);

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
            IWeaponService weaponService,
            ITargetProvider targetProvider
        );
    }
}