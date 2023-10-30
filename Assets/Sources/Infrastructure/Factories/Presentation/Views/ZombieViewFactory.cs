using Sources.Controllers.Zombies;
using Sources.Domain.Systems;
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
        private readonly MovementSystemViewFactory _movementSystemViewFactory;

        public ZombieViewFactory(
            ZombiePresenterFactory zombiePresenterFactory,
            MovementSystemViewFactory movementSystemViewFactory
            )
        {
            _zombiePresenterFactory = zombiePresenterFactory;
            _movementSystemViewFactory = movementSystemViewFactory;
        }

        public ZombieView Create(Zombie zombie)
        {
            ZombieView view = Object.Instantiate(Resources.Load<ZombieView>("Views/Zombies/ZombieView"));
            ZombiePresenter presenter = _zombiePresenterFactory.Create(view, zombie);
            view.Construct(presenter);
            
            _movementSystemViewFactory.Create(view.gameObject, new MovementSystem());
            
            return view;
        }
    }
}