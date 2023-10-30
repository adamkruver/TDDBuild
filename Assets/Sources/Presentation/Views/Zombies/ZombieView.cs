using Sources.Controllers.Zombies;
using Sources.PresentationInterfaces.Views.Zombies;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Zombies
{
    public class ZombieView : MonoBehaviour, IZombieView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _destination;
        
        private ZombiePresenter _presenter;

        private void Start() => 
            _navMeshAgent.destination = _destination.position;

        public void Update()
        {
            if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
                _navMeshAgent.Stop();
        }

        public void Construct(ZombiePresenter presenter) => 
            _presenter = presenter;
    }
}