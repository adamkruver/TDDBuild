namespace Sources.PresentationInterfaces.Views.Weapons
{
    public interface IWeaponView
    {
        void Shoot();
        float GunPointOffset { get; }
    }
}