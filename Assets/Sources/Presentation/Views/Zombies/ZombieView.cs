using Cysharp.Threading.Tasks;
using Sources.Controllers.Zombies;
using Sources.Presentation.Animations.Enemies;
using Sources.Presentation.Views.Enemies;
using Sources.Presentation.Views.HealthPoints;
using Sources.PresentationInterfaces.Views.Zombies;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Zombies
{
    public class ZombieView : EnemyView<IZombieStateMachine>, IZombieView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private ZombieAnimation _zombieAnimation;

        [field: SerializeField] public HealthView Health { get; private set; }


        public override float Speed => _navMeshAgent.speed;

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

        public UniTask Fall(float lastHitForwardProjection) =>
            _zombieAnimation.Fall(lastHitForwardProjection);

        public void DisablePhysics()
        {
            _navMeshAgent.enabled = false;
            IsVisible = false;
        }

        public UniTask Decay() =>
            _zombieAnimation.Decay();

        protected override void OnAfterCreate()
        {
            base.OnAfterCreate();
            _navMeshAgent.enabled = true;
            IsVisible = true;
            _zombieAnimation.ResetToIdle();
        }
    }
}