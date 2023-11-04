using Sources.Controllers.Zombies;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Presentation.Systems;
using Sources.Infrastructure.ObjectPools;
using Sources.Presentation.Views.Zombies;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class ZombieViewFactory
    {
        private readonly ZombieStateMachineFactory _zombieStateMachineFactory;
        private readonly MovementSystemViewFactory _movementSystemViewFactory;
        private readonly DamageableSystemViewFactory _damageableSystemViewFactory;
        private readonly BaseView _baseView;
        private readonly ZombieView _prefab;
        private readonly ObjectPool _objectPool = new ObjectPool();

        public ZombieViewFactory(
            ZombieStateMachineFactory zombieStateMachineFactory,
            MovementSystemViewFactory movementSystemViewFactory,
            DamageableSystemViewFactory damageableSystemViewFactory,
            BaseView baseView
        )
        {
            _zombieStateMachineFactory = zombieStateMachineFactory;
            _movementSystemViewFactory = movementSystemViewFactory;
            _damageableSystemViewFactory = damageableSystemViewFactory;
            _baseView = baseView;
            _prefab = Resources.Load<ZombieView>("Views/Zombies/ZombieView");
        }

        public ZombieView Create(Zombie zombie, Vector3 spawnPosition)
        {
            ZombieView view = _objectPool.Get<ZombieView>() ?? Object.Instantiate(_prefab, spawnPosition, Quaternion.identity);
            view.Create(_objectPool);
            view.SetPosition(spawnPosition);
            
            ZombieStateMachine zombieStateMachine = _zombieStateMachineFactory.Create(view, zombie,_baseView);
            
            view.Construct(zombieStateMachine);

            _movementSystemViewFactory.Create(view.gameObject, zombie.MovementSystem);
            _damageableSystemViewFactory.Create(view.DamageableSystemView, zombie.Health);

            return view;
        }
    }
}