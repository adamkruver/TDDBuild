using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class WeaponRotationSystem : MonoBehaviour, IWeaponRotationSystem
    {
        [SerializeField] private Transform _base;
        [SerializeField] private Transform _head;

        public Vector3 Position => _base.position;

        public void SetBaseLookDirection(Vector3 lookDirection) =>
            _base.forward = lookDirection;

        public void SetXAngle(float angle) =>
            _head.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));
    }
}