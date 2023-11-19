using System;
using System.Linq;
using Sources.Domain.Projectiles;
using Sources.Domain.Weapons;
using Sources.Domain.Weapons.Lasers;
using Sources.Infrastructure.Factories.Domain.Projectiles;
using Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons.Lasers
{
    public abstract class LaserWeaponStateMachineFactoryBase : WeaponStateMachineFactoryBase
    {
        private readonly ILaserViewFactory _laserViewFactory;
        private readonly LaserFactory _laserFactory;

        protected LaserWeaponStateMachineFactoryBase(ILaserViewFactory laserViewFactory, LaserFactory laserFactory)
        {
            _laserViewFactory = laserViewFactory ?? throw new ArgumentNullException(nameof(laserViewFactory));
            _laserFactory = laserFactory ?? throw new ArgumentNullException(nameof(laserFactory));
        }

        protected sealed override IFiniteState CreateStates(
            IWeaponView view,
            IWeapon weapon,
            IWeaponService weaponService,
            ITargetProvider targetProvider
        )
        {
            if (weapon is not ILaserWeapon laserWeapon)
                throw new InvalidCastException(nameof(weapon));

            Laser[] lasers = Enumerable.Repeat(_laserFactory.Create(), view.ShootPoints.Length).ToArray();
            laserWeapon.SetLasers(lasers);

            for (int i = 0; i < laserWeapon.Lasers.Length; i++)
                view.ShootPoints[i].SetProjectile(_laserViewFactory.Create(laserWeapon.Lasers[i]));

            return CreateStates(
                view,
                laserWeapon,
                weaponService,
                targetProvider
            );
        }

        protected abstract IFiniteState CreateStates(
            IWeaponView view,
            ILaserWeapon weapon,
            IWeaponService weaponService,
            ITargetProvider targetProvider
        );
    }
}