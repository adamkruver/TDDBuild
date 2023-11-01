using Sources.PresentationInterfaces.Animations.Weapons;

namespace Sources.PresentationInterfaces.Views.Weapons
{
    public interface IWeaponView
    {
        IWeaponRotationSystem RotationSystem { get; }
        IWeaponAnimation Animation { get; }

        void Fire();
    }
}