using System;
using Sources.PresentationInterfaces.Views.Systems.Movements;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Systems.Movements
{
    public class MovementSystemView : MonoBehaviour, IMovementSystemView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private MonoBehaviour _animation;

        private IMovementAnimation _movementAnimation;

        private void Awake() =>
            _movementAnimation = _animation as IMovementAnimation
                                 ?? throw new NullReferenceException(nameof(_animation));

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

        public void SetSpeed(float speed)
        {
            _navMeshAgent.speed = speed;
            _movementAnimation.SetSpeed(speed);
        }
    }
}