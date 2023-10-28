using Sources.Controllers.Weapons;
using Sources.Domain.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Infrastructure.Factories.Controllers.Weapons
{
    public class WeaponPresenterFactory
    {
        public WeaponPresenter Create(IWeaponView view, IWeapon weapon)
        {
            return new WeaponPresenter(view, weapon);
        }
    }
}