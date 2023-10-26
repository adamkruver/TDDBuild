using UnityEngine;

namespace Sources.Presentation.Views.Cameras
{
    public class GameplayCamera : MonoBehaviour
    {
        [SerializeField] private float _xAxisMax = 5f;
        private Transform _transform;

        public Vector3 Angles { get; private set; }

        private void Awake() => 
            _transform = GetComponent<Transform>();

        public void Rotate(Vector2 angles)
        {
            angles.x = Mathf.Clamp(angles.x, -_xAxisMax, _xAxisMax);
            _transform.eulerAngles = angles;
            Angles = angles;
        }
    }
}