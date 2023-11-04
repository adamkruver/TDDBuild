using Sources.PresentationInterfaces.Animations.Weapons;

namespace Sources.PresentationInterfaces.Views.Weapons
{
    public interface IWeaponView
    {
        IWeaponAnimation Animation { get; }

        void Fire();
    }
}