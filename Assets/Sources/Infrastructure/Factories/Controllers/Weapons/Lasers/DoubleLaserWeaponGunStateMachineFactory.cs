using Sources.Controllers.Weapons.StateMachines.Common.States;
using Sources.Controllers.Weapons.StateMachines.Common.Transitions;
using Sources.Controllers.Weapons.StateMachines.Lasers.States;
using Sources.Controllers.Weapons.StateMachines.Lasers.Transitions;
using Sources.Domain.Weapons.Lasers;
using Sources.Infrastructure.Factories.Domain.Projectiles;
using Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons.Lasers
{
    public class DoubleLaserWeaponGunStateMachineFactory : LaserWeaponStateMachineFactoryBase
    {
        public DoubleLaserWeaponGunStateMachineFactory(ILaserViewFactory laserViewFactory, LaserFactory laserFactory) : base(
            laserViewFactory, laserFactory
        )
        {
        }

        protected override IFiniteState CreateStates(
            IWeaponView view,
            ILaserWeapon weapon,
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