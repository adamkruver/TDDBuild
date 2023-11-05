using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Presentation.Views.Weapons
{
    public interface ICompositeWeaponView
    {
        IWeaponRotationSystem RotationSystem { get; }
        int BarrelsAmount { get; }
        float GunPointOffset { get; }
        
        void Shoot(int weaponId);
    }
}