using System;
using Sources.Controllers.Weapons.StateMachines.Common.States;
using Sources.Controllers.Weapons.StateMachines.Common.Transitions;
using Sources.Controllers.Weapons.StateMachines.Lasers.States;
using Sources.Controllers.Weapons.StateMachines.Lasers.Transitions;
using Sources.Domain.Weapons;
using Sources.Domain.Weapons.Rockets;
using Sources.Infrastructure.Factories.Domain.Projectiles;
using Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons.Rockets
{
    public class RocketTwiceGunStateMachineFactory : RocketWeaponStateMachineFactoryBase
    {
        public RocketTwiceGunStateMachineFactory(IRocketViewFactory rocketViewFactory, RocketFactory rocketFactory) :
            base(rocketViewFactory, rocketFactory)
        {
        }

        protected override IFiniteState CreateStates(
            IWeaponView view,
            IRocketWeapon weapon,
            IWeaponService weaponService,
            ITargetProvider targetProvider
        )
        {
            RocketTwiceGun rocketWeapon = weapon as RocketTwiceGun ?? throw new InvalidCastException(nameof(weapon));

            TrackTargetState trackTargetState = new TrackTargetState(
                weapon,
                weaponService,
                view,
                targetProvider
            );

            RocketShootState shootState = new RocketShootState(rocketWeapon, view, targetProvider);

            ToShootStateTransition toShootStateTransition = new ToShootStateTransition(
                shootState,
                weapon,
                targetProvider,
                weaponService,
                view
            );

            RocketReloadState rocketReloadState = new RocketReloadState(
                view, rocketWeapon, RocketFactory, RocketViewFactory
            );

            CooldownState cooldownState = new CooldownState();
            ToCooldownTransition toCooldownTransition = new ToCooldownTransition(cooldownState, weapon);
            ToTrackTargetTransition toTrackTargetTransition = new ToTrackTargetTransition(trackTargetState, weapon);
            ToReloadTransition toReloadTransition = new ToReloadTransition(rocketReloadState, rocketWeapon);

            rocketReloadState.AddTransition(toTrackTargetTransition);
            trackTargetState.AddTransition(toShootStateTransition);
            shootState.AddTransition(toCooldownTransition);
            cooldownState.AddTransition(toReloadTransition);

            return rocketReloadState;
        }
    }
}