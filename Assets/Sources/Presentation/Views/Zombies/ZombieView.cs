using Sources.Controllers.Zombies;
using Sources.Presentation.Views.Enemies;
using Sources.PresentationInterfaces.Views.Zombies;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Zombies
{
    public class ZombieView : EnemyView<ZombiePresenter>, IZombieView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public void Update()
        {
            if (_navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
                _navMeshAgent.Stop();
        }

        public void SetDestination(Vector3 position) =>
            _navMeshAgent.SetDestination(position);

        public void Die()
        {
            gameObject.SetActive(false);
            Debug.Log("Zombie Died");
        }
    }
}