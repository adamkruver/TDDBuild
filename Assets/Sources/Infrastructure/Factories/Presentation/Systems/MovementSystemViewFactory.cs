using System;
using Sources.Controllers.Systems;
using Sources.Domain.Systems;
using Sources.Infrastructure.Factories.Controllers.Systems;
using Sources.Presentation.Views.Systems.Movements;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Systems
{
    public class MovementSystemViewFactory
    {
        private readonly MovementSystemPresenterFactory _movementSystemPresenterFactory;

        public MovementSystemViewFactory(MovementSystemPresenterFactory movementSystemPresenterFactory)
        {
            _movementSystemPresenterFactory = movementSystemPresenterFactory;
        }

        public MovementSystemView Create(GameObject gameObject, MovementSystem system)
        {
            MovementSystemView view = gameObject.GetComponentInChildren<MovementSystemView>()
                                                    ?? throw new NullReferenceException(nameof(MovementSystemView));
            
            MovementSystemPresenter presenter = _movementSystemPresenterFactory.Create(view, system);
            view.Construct(presenter);

            return view;
        }
    }
}