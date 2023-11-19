using System;
using JetBrains.Annotations;
using Sources.Domain.Weapons;
using Sources.InfrastructureInterfaces.Providers;
using Sources.InfrastructureInterfaces.Services.Times;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public abstract class WeaponFactoryBase<T> : IWeaponFactory where T : IWeapon
    {
        protected readonly ITimeService TimeService;
        protected readonly WeaponFab WeaponFab;

        protected WeaponFactoryBase(IResourceProvider resourceProvider, [NotNull] ITimeService timeService)
        {
            TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));

            if (resourceProvider == null)
                throw new ArgumentNullException(nameof(resourceProvider));

            WeaponFab = resourceProvider.Load<WeaponFab>($"Fabs/Weapons/{typeof(T).Name}Fab") ??
                        throw new InvalidOperationException(nameof(resourceProvider.Load));
        }

        public abstract IWeapon Create();
    }
}