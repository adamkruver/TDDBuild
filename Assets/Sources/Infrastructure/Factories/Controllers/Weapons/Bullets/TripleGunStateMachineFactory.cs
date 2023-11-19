using Sources.Controllers.Weapons.StateMachines.Common.States;
using Sources.Controllers.Weapons.StateMachines.Common.Transitions;
using Sources.Controllers.Weapons.StateMachines.Lasers.States;
using Sources.Controllers.Weapons.StateMachines.Lasers.Transitions;
using Sources.Domain.Weapons.Bullets;
using Sources.Infrastructure.Factories.Domain.Projectiles;
using Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons.Bullets
{
    public class TripleGunStateMachineFactory : BulletWeaponStateMachineFactoryBase
    {
        public TripleGunStateMachineFactory(IBulletViewFactory bulletViewFactory, BulletFactory bulletFactory) : base(
            bulletViewFactory, bulletFactory
        )
        {
        }

        protected sealed override IFiniteState CreateStates(
            IWeaponView view,
            IBulletWeapon weapon,
            IWeaponService weaponService,
            ITargetProvider targetProvider
        )
        {
            TrackTargetState trackTargetState = new TrackTargetState(
                weapon,
                weaponService,
                view,
                targetProvider
            );

            ShootState shootState = new ShootState(weapon);

            ToShootStateTransition toShootStateTransition = new ToShootStateTransition(
                shootState,
                weapon,
                targetProvider,
                weaponService,
                view
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