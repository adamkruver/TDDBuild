using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.PresentationInterfaces.Views.Zombies;
using UnityEngine;

namespace Sources.Controllers.Zombies.FiniteStateMachines.States
{
    public class MoveState : FiniteStateBase
    {
        private readonly IZombieView _zombieView;
        private readonly Vector3 _destination;

        public MoveState(IZombieView zombieView, Vector3 destination)
        {
            _zombieView = zombieView;
            _destination = destination;
        }

        protected override void OnEnter() => 
            _zombieView.SetDestination(_destination);
    }
}