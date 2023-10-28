using Sources.Domain.Weapons;
using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Controllers.Weapons
{
    public class WeaponPresenter
    {
        private readonly IWeaponView _view;
        private readonly IWeapon _weapon;

        public WeaponPresenter(IWeaponView view, IWeapon weapon)
        {
            _view = view;
            _weapon = weapon;
        }
    }
}