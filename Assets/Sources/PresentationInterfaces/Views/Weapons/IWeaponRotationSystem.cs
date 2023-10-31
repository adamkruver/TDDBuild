using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Weapons
{
    public interface IWeaponRotationSystem
    {
        Vector3 Position { get; }

        void SetBaseLookDirection(Vector3 lookDirection);

        void SetXAngle(float angle);
    }
}