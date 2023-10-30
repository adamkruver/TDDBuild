using Sources.PresentationInterfaces.Views.Systems.Movements;
using UnityEngine;

namespace Sources.Presentation.Views.Systems.Movements
{
    public class MovementAnimation : MonoBehaviour, IMovementAnimation
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _speedDivider = 3f;

        private static readonly int s_speedHash = Animator.StringToHash("Speed");

        public void SetSpeed(float speed) =>
            _animator.SetFloat(s_speedHash, ClampSpeed(speed));

        private float ClampSpeed(float speed) =>
            Mathf.Clamp(speed, 0, _speedDivider) / _speedDivider;
    }
}