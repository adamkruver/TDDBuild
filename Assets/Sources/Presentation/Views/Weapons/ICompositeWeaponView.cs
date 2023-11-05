using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public interface ICompositeWeaponView
    {
        IWeaponRotationSystem RotationSystem { get; }
        int BarrelsAmount { get; }
        float GunPointOffset { get; }
        Vector3 HeadPosition { get; }
        
        void Shoot(int weaponId);
    }
}