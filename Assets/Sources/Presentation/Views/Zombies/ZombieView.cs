using Sources.Controllers.Zombies;
using Sources.Presentation.Animations.Enemies;
using Sources.Presentation.Views.Enemies;
using Sources.Presentation.Views.HealthPoints;
using Sources.PresentationInterfaces.Views.Zombies;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Zombies
{
    public class ZombieView : EnemyView<ZombieStateMachine>, IZombieView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private ZombieAnimation _zombieAnimation;

        protected override void OnConstruct()
        {
            base.OnConstruct();
            _zombieAnimation.ResetToIdle();
            IsVisible = true;
        }

        [field: SerializeField] public HealthView Health { get; private set; }
        
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

        public void Hit(float lastHitForwardProjection) => 
            _zombieAnimation.Hit(lastHitForwardProjection);

        public async void Die(float lastHitForwardProjection)
        {
            Presenter.Disable();
            Presenter = null;
            
            IsVisible = false;
            
            await _zombieAnimation.Die(lastHitForwardProjection);
            
            Destroy();
        }
    }
}