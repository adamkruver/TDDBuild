using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Weapons
{
    public interface IWeaponRotationSystem
    {
        Vector3 Position { get; }

        void UpdateRotationBase(Vector3 lookDirection, float rotationSpeed);

        void SetXAngle(float angle);
        bool HasTargetAtLook(Vector3 getDirectionToEnemy);
    }
}