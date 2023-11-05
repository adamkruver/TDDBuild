using Sources.Controllers.Zombies;
using Sources.Presentation.Views.Enemies;
using Sources.PresentationInterfaces.Views.Zombies;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Zombies
{
    public class ZombieView : EnemyView<ZombieStateMachine>, IZombieView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public void Update() =>
            Presenter?.Update(Time.deltaTime);

        public void SetDestination(Vector3 destination) =>
            _navMeshAgent.SetDestination(destination);

        public void Stop()
        {
            if (_navMeshAgent.isActiveAndEnabled == false)
                return;
            
            _navMeshAgent.isStopped = true;
        }

        public void SetPosition(Vector3 spawnPosition) =>
            Transform.position = spawnPosition;
        
        public void Die()
        {
            Presenter.Disable();
            Presenter = null;
            Destroy();
        }
    }
}