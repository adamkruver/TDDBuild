using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Weapons
{
    public interface IWeaponView
    {
        IShootPointView[] ShootPoints { get; }
        IWeaponRotationSystem RotationSystem { get; }
        int BarrelsAmount { get; }
        float GunPointOffset { get; }
        Vector3 HeadPosition { get; }
        
        void Shoot();
        Vector3 GetShootPointPosition();
        void SetActiveShootPoint(int index);
    }
}