using System;
using Sources.Controllers.Zombies;
using Sources.Domain.Systems;
using Sources.Domain.Zombies;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Infrastructure.Factories.Controllers.Zombies;
using Sources.Infrastructure.Factories.Presentation.Systems;
using Sources.Presentation.Views.Zombies;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Presentation.Views
{
    public class ZombieViewFactory
    {
        private readonly ZombiePresenterFactory _zombiePresenterFactory;
        private readonly MovementSystemViewFactory _movementSystemViewFactory;
        private readonly DamageableSystemViewFactory _damageableSystemViewFactory;
        private readonly BaseView _baseView;

        public ZombieViewFactory(
            ZombiePresenterFactory zombiePresenterFactory,
            MovementSystemViewFactory movementSystemViewFactory,
            DamageableSystemViewFactory damageableSystemViewFactory,
            BaseView baseView
        )
        {
            _zombiePresenterFactory = zombiePresenterFactory;
            _movementSystemViewFactory = movementSystemViewFactory;
            _damageableSystemViewFactory = damageableSystemViewFactory;
            _baseView = baseView;
        }

        public ZombieView Create(Zombie zombie)
        {
            ZombieView view = Object.Instantiate(Resources.Load<ZombieView>("Views/Zombies/ZombieView"));
            ZombiePresenter presenter = _zombiePresenterFactory.Create(view, zombie);
            view.Construct(presenter);

            _movementSystemViewFactory.Create(view.gameObject, zombie.MovementSystem);
            _damageableSystemViewFactory.Create(view.DamageableSystemView, zombie.Health);
            
            view.SetDestination(_baseView.DoorsPosition);
            
            return view;
        }
    }
}