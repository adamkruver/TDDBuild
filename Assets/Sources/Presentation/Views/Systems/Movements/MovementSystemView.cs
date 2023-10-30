using System;
using Sources.Controllers.Systems;
using Sources.PresentationInterfaces.Views.Systems.Movements;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Systems.Movements
{
    public class MovementSystemView : MonoBehaviour, IMovementSystemView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private MonoBehaviour _animation;
        [SerializeField] private float _speedDivider = 2.5f;

        private IMovementAnimation _movementAnimation;
        private MovementSystemPresenter _presenter;

        private void Awake() =>
            _movementAnimation = _animation as IMovementAnimation
                                 ?? throw new NullReferenceException(nameof(_animation));

        private void OnEnable() =>
            _presenter?.Enable();

        private void OnDisable() =>
            _presenter?.Disable();

        private void OnValidate()
        {
            if (_animation is not IMovementAnimation movementAnimation)
            {
                _animation = null;
                _movementAnimation = null;

                return;
            }

            _movementAnimation = movementAnimation;
        }

        public void Construct(MovementSystemPresenter presenter)
        {
            gameObject.SetActive(false);
            _presenter = presenter;
            gameObject.SetActive(true);
        }

        public void SetSpeed(float speed)
        {
            _navMeshAgent.speed = speed / _speedDivider;
            _movementAnimation.SetSpeed(speed);
        }
    }
}