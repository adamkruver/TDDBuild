using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class WeaponRotationSystem : MonoBehaviour, IWeaponRotationSystem
    {
        [SerializeField] private Transform _base;
        [SerializeField] private Transform _head;

        public Vector3 Position => _base.position;

        public void SetXAngle(float angle) =>
            _head.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));

        public bool HasTargetAtLook(Vector3 lookDirection) =>
            Vector3.Angle(_base.forward, lookDirection) < 1;

        public void UpdateRotationBase(Vector3 lookDirection, float rotationSpeed)
        {
            if (Vector3.Angle(_base.forward, lookDirection) < 3)
            {
                _base.forward = lookDirection;

                return;
            }

            _base.forward = Vector3.MoveTowards(_base.forward, lookDirection, Time.deltaTime / rotationSpeed);
        }
    }
}