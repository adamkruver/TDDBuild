using Sources.Controllers.Zombies;
using Sources.PresentationInterfaces.Views.Zombies;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Zombies
{
    public class ZombieView : MonoBehaviour, IZombieView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private ZombiePresenter _presenter;

        private void OnEnable() =>
            _presenter?.Enable();

        private void OnDisable() =>
            _presenter?.Disable();

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

        public void Construct(ZombiePresenter presenter)
        {
            gameObject.SetActive(false);
            _presenter = presenter;
            gameObject.SetActive(true);
        }
    }
}