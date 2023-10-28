using Sources.Presentation.Views.Tilemaps;
using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Turrets;

namespace Sources.Presentation.Views.Turrets
{
    public class TurretView : GridCellView, ITurretView
    {
        public void SetWeapon(WeaponView weaponView) =>
            weaponView.SetParent(Transform);
    }
}