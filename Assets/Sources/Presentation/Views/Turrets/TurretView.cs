using Sources.Presentation.Views.Weapons;
using Sources.PresentationInterfaces.Views.Turrets;
using UnityEngine;

namespace Sources.Presentation.Views.Turrets
{
    public class TurretView : MonoBehaviour, ITurretView
    {
        private Transform _transform;

        private void Awake() =>
            _transform = GetComponent<Transform>();

        public void SetWeapon(WeaponView weaponView) =>
            weaponView.SetParent(_transform);

        public void SetPosition(Vector2Int position) =>
            _transform.position = new Vector3(position.x, 0, position.y) * 2 + new Vector3(1, 0, 1);
    }
}