using Sources.Controllers.Weapons.StateMachines.Lasers.States;
using Sources.Controllers.Weapons.StateMachines.Lasers.Transitions;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public class SingleGunStateMachineFactory : WeaponStateMachineFactoryBase
    {
        protected override IFiniteState CreateStates(ICompositeWeaponView compositeView,
            IWeapon weapon,
            IWeaponService weaponService,
            ITargetProvider targetProvider
        )
        {
            TrackTargetState trackTargetState = new TrackTargetState(
                weapon,
                weaponService,
                compositeView,
                targetProvider
            );

            ShootState shootState = new ShootState(compositeView, weapon, shootsAtOnce: 1);

            ToShootStateTransition toShootStateTransition = new ToShootStateTransition(
                shootState,
                weapon,
                targetProvider,
                weaponService,
                compositeView
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