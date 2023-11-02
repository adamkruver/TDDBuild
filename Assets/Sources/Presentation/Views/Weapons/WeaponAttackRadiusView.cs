using UnityEngine;

namespace Sources.Presentation.Views.Weapons
{
    public class WeaponAttackRadiusView : MonoBehaviour
    {
        private Transform _transform;

        private void Awake() =>
            _transform = GetComponent<Transform>();

        public void SetRadius(float radius) =>
            _transform.localScale = radius * Vector3.one;
    }
}