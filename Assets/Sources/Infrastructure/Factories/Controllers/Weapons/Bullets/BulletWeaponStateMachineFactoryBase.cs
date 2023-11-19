using System;
using System.Linq;
using JetBrains.Annotations;
using Sources.Domain.Projectiles;
using Sources.Domain.Weapons;
using Sources.Domain.Weapons.Bullets;
using Sources.Domain.Weapons.Lasers;
using Sources.Infrastructure.Factories.Domain.Projectiles;
using Sources.InfrastructureInterfaces.Factories.Presentation.Projectiles;
using Sources.InfrastructureInterfaces.FiniteStateMachines;
using Sources.InfrastructureInterfaces.Services.Weapons;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons.Bullets
{
    public abstract class BulletWeaponStateMachineFactoryBase : WeaponStateMachineFactoryBase
    {
        private readonly IBulletViewFactory _bulletViewFactory;
        private readonly BulletFactory _bulletFactory;

        protected BulletWeaponStateMachineFactoryBase(IBulletViewFactory bulletViewFactory, BulletFactory bulletFactory)
        {
            _bulletViewFactory = bulletViewFactory ?? throw new ArgumentNullException(nameof(bulletViewFactory));
            _bulletFactory = bulletFactory ?? throw new ArgumentNullException(nameof(bulletFactory));
        }

        protected sealed override IFiniteState CreateStates(
            IWeaponView view,
            IWeapon weapon,
            IWeaponService weaponService,
            ITargetProvider targetProvider
        )
        {
            if (weapon is not IBulletWeapon bulletWeapon)
                throw new InvalidCastException(nameof(weapon));

            Bullet[] bullets = Enumerable.Repeat(_bulletFactory.Create(), view.ShootPoints.Length).ToArray();
            bulletWeapon.SetBullets(bullets);

            for (int i = 0; i < bulletWeapon.Bullets.Length; i++)
                view.ShootPoints[i].SetProjectile(_bulletViewFactory.Create(bulletWeapon.Bullets[i]));

            return CreateStates(
                view,
                bulletWeapon,
                weaponService,
                targetProvider
            );
        }

        protected abstract IFiniteState CreateStates(
            IWeaponView view,
            IBulletWeapon weapon,
            IWeaponService weaponService,
            ITargetProvider targetProvider
        );
    }
}