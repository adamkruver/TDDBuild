using Sources.Controllers.Zombies;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Presentation.Systems;
using Sources.Infrastructure.ObjectPools;
using Sources.Infrastructure.Resource;
using Sources.Presentation.Views.Zombies;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class ZombieViewFactory
    {
        private readonly ResourceService _resourceService;
        private readonly ZombieStateMachineFactory _zombieStateMachineFactory;
        private readonly MovementSystemViewFactory _movementSystemViewFactory;
        private readonly DamageableSystemViewFactory _damageableSystemViewFactory;
        private readonly BaseView _baseView;
        private readonly ObjectPool _objectPool = new ObjectPool();

        public ZombieViewFactory(
            ResourceService resourceService,
            ZombieStateMachineFactory zombieStateMachineFactory,
            MovementSystemViewFactory movementSystemViewFactory,
            DamageableSystemViewFactory damageableSystemViewFactory,
            BaseView baseView
        )
        {
            _resourceService = resourceService;
            _zombieStateMachineFactory = zombieStateMachineFactory;
            _movementSystemViewFactory = movementSystemViewFactory;
            _damageableSystemViewFactory = damageableSystemViewFactory;
            _baseView = baseView;
        }

        public ZombieView Create(Zombie zombie, Vector3 spawnPosition)
        {
            ZombieView view = _objectPool.Get<ZombieView>() ?? Object.Instantiate(
                _resourceService.Load<ZombieView>("Views/Zombies/ZombieView"), spawnPosition, Quaternion.identity
            );
            view.Create(_objectPool);
            view.SetPosition(spawnPosition);

            ZombieStateMachine zombieStateMachine = _zombieStateMachineFactory.Create(view, zombie, _baseView);

            view.Construct(zombieStateMachine);

            _movementSystemViewFactory.Create(view.gameObject, zombie.MovementSystem);
            _damageableSystemViewFactory.Create(view.DamageableSystemView, zombie.Health);

            return view;
        }
    }
}