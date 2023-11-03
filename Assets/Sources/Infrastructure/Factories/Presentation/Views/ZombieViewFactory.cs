using Sources.Controllers.Zombies;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Presentation.Systems;
using Sources.Presentation.Views.Zombies;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class ZombieViewFactory
    {
        private readonly ZombiePresenterFactory _zombiePresenterFactory;
        private readonly ZombieStateMachineFactory _zombieStateMachineFactory;
        private readonly MovementSystemViewFactory _movementSystemViewFactory;
        private readonly DamageableSystemViewFactory _damageableSystemViewFactory;
        private readonly BaseView _baseView;
        private readonly ZombieView _prefab;

        public ZombieViewFactory(
            ZombiePresenterFactory zombiePresenterFactory,
            ZombieStateMachineFactory zombieStateMachineFactory,
            MovementSystemViewFactory movementSystemViewFactory,
            DamageableSystemViewFactory damageableSystemViewFactory,
            BaseView baseView
        )
        {
            _zombiePresenterFactory = zombiePresenterFactory;
            _zombieStateMachineFactory = zombieStateMachineFactory;
            _movementSystemViewFactory = movementSystemViewFactory;
            _damageableSystemViewFactory = damageableSystemViewFactory;
            _baseView = baseView;
            _prefab = Resources.Load<ZombieView>("Views/Zombies/ZombieView");
        }

        public ZombieView Create(Zombie zombie, Vector3 spawnPosition)
        {
            ZombieView view = Object.Instantiate(_prefab, spawnPosition, Quaternion.identity);
            // ZombiePresenter presenter = _zombiePresenterFactory.Create(view, zombie);
            ZombieStateMachine zombieStateMachine = _zombieStateMachineFactory.Create(view, zombie,_baseView);
            
            view.Construct(zombieStateMachine);
            // view.Construct(presenter);

            _movementSystemViewFactory.Create(view.gameObject, zombie.MovementSystem);
            _damageableSystemViewFactory.Create(view.DamageableSystemView, zombie.Health);

            // view.SetDestination(_baseView.DoorsPosition);

            return view;
        }
    }
}