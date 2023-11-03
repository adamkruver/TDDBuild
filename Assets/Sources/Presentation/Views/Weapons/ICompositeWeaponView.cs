using Sources.PresentationInterfaces.Views.Weapons;

namespace Sources.Presentation.Views.Weapons
{
    public interface ICompositeWeaponView
    {
        IWeaponRotationSystem RotationSystem { get; }
    }
}