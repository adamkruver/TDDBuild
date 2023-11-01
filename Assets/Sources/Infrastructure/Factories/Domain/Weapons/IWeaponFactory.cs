using Sources.Domain.Weapons;

namespace Sources.Infrastructure.Factories.Domain.Weapons
{
    public interface IWeaponFactory
    {
        IWeapon Create();
    }
}