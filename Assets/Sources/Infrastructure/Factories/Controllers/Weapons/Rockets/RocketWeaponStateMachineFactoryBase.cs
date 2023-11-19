using System;
using Sources.Domain.Weapons;
using Sources.Domain.Weapons.Rockets;
using Sources.Infrastructure.Factories.Domain.Projectiles;
using Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons.Rockets
{
    public abstract class RocketWeaponStateMachineFactoryBase : WeaponStateMachineFactoryBase
    {
        protected readonly IRocketViewFactory RocketViewFactory;
        protected readonly RocketFactory RocketFactory;

        protected RocketWeaponStateMachineFactoryBase(IRocketViewFactory rocketViewFactory, RocketFactory rocketFactory)
        {
            RocketViewFactory = rocketViewFactory ?? throw new ArgumentNullException(nameof(rocketViewFactory));
            RocketFactory = rocketFactory ?? throw new ArgumentNullException(nameof(rocketFactory));
        }

        protected sealed override IFiniteState CreateStates(
            IWeaponView view,
            IWeapon weapon,
            IWeaponService weaponService,
            ITargetProvider targetProvider
        )
        {
            if (weapon is not IRocketWeapon rocketWeapon)
                throw new InvalidCastException(nameof(weapon));

            return CreateStates(
                view,
                rocketWeapon,
                weaponService,
                targetProvider
            );
        }

        protected abstract IFiniteState CreateStates(
            IWeaponView view,
            IRocketWeapon weapon,
            IWeaponService weaponService,
            ITargetProvider targetProvider
        );
    }
}