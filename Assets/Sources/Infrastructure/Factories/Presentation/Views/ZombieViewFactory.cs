using Sources.Controllers.Zombies.FiniteStateMachines;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Presentation.Systems;
using Sources.Infrastructure.ObjectPools;
using Sources.Infrastructure.Resource;
using Sources.Presentation.Views.Cameras;
using Sources.Presentation.Views.Zombies;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class ZombieViewFactory
    {
        private readonly HealthViewFactory _healthViewFactory;
        private readonly ResourceService _resourceService;
        private readonly ZombieStateMachineFactory _zombieStateMachineFactory;
        private readonly MovementSystemViewFactory _movementSystemViewFactory;
        private readonly DamageableSystemViewFactory _damageableSystemViewFactory;
        private readonly GameplayCamera _gameplayCamera;
        private readonly BaseView _baseView;
        private readonly ObjectPool _objectPool;

        public ZombieViewFactory(
            HealthViewFactory healthViewFactory,
            ResourceService resourceService,
            ZombieStateMachineFactory zombieStateMachineFactory,
            MovementSystemViewFactory movementSystemViewFactory,
            DamageableSystemViewFactory damageableSystemViewFactory,
            GameplayCamera gameplayCamera,
            BaseView baseView
        )
        {
            _healthViewFactory = healthViewFactory;
            _resourceService = resourceService;
            _zombieStateMachineFactory = zombieStateMachineFactory;
            _movementSystemViewFactory = movementSystemViewFactory;
            _damageableSystemViewFactory = damageableSystemViewFactory;
            _gameplayCamera = gameplayCamera;
            _baseView = baseView;
            _objectPool = new ObjectPool(" ==== Enemy Pool ==== ");
        }

        private ZombieView Prefab =>
            _resourceService.Load<ZombieView>("Views/Zombies/ZombieView");

        public ZombieView Create(Zombie zombie, Vector3 spawnPosition)
        {
            if (_objectPool.Contain<ZombieView>() == false)
                Object.Instantiate(Prefab).SetPool(_objectPool).Destroy();

            ZombieView view = _objectPool.Get<ZombieView>();

            _healthViewFactory.Create(view.Health, zombie.Health);

            view.Health.SetCamera(_gameplayCamera.Camera);
            view.SetPool(_objectPool);
            view.SetPosition(spawnPosition);

            ZombieStateMachine zombieStateMachine = _zombieStateMachineFactory.Create(view, zombie, _baseView);

            view.Construct(zombieStateMachine);

            _damageableSystemViewFactory.Create(view.DamageableSystemView, zombie.Health);

            return view;
        }
    }
}