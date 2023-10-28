using Sources.PresentationInterfaces.Views.Weapons;
using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class WeaponView : MonoBehaviour, IWeaponView
    {
        private Transform _transform;

        private void Awake() =>
            _transform = GetComponent<Transform>();

        public void SetParent(Transform parent) =>
            _transform.SetParent(parent, true);
    }
}