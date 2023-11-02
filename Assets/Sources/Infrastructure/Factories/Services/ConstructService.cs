using System;
using Sources.Infrastructure.Factories.Handlers;
using Sources.Infrastructure.Handlers.Pointers;
using Sources.Infrastructure.Services.Payments;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.InfrastructureInterfaces.Services.Pointers;
using Sources.Presentation.Views.Cameras;
using Sources.PresentationInterfaces.Views.Constructions;

namespace Sources.Infrastructure.Factories.Services
{
    public class ConstructService
    {
        private readonly IPointerService _pointerService;
        private readonly GameplayCamera _gameplayCamera;
        private readonly TilemapUntouchablePointerHandlerFactory _tilemapUntouchablePointerHandlerFactory;
        private readonly PaymentService _paymentService;
        private readonly TilemapService _tilemapService;

        private IConstructionView _constructionView;

        public ConstructService(
            IPointerService pointerService,
            PaymentService paymentService,
            TilemapService tilemapService,
            GameplayCamera gameplayCamera,
            TilemapUntouchablePointerHandlerFactory tilemapUntouchablePointerHandlerFactory
        )
        {
            _pointerService = pointerService ?? throw new ArgumentNullException(nameof(pointerService));
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _tilemapService = tilemapService ?? throw new ArgumentNullException(nameof(tilemapService));
            _gameplayCamera = gameplayCamera ?? throw new ArgumentNullException(nameof(gameplayCamera));
            _tilemapUntouchablePointerHandlerFactory = tilemapUntouchablePointerHandlerFactory
                                                       ?? throw new ArgumentNullException(
                                                           nameof(tilemapUntouchablePointerHandlerFactory)
                                                       );
        }

        public void Enable(IConstructionView constructionView, int money)
        {
            Disable();

            _constructionView = constructionView;

            _pointerService.RegisterHandler(
                0,
                new GameplayInteractPointerHandler(
                    _gameplayCamera, position =>
                    {
                        if (_paymentService.TryPay(money))
                            constructionView.Build(_tilemapService.ConvertToWorldPosition(position));

                        Disable();
                    }
                )
            );

            _pointerService.RegisterUntouchableHandler(
                _tilemapUntouchablePointerHandlerFactory.Create(constructionView)
            );
        }

        public void Disable()
        {
            _pointerService.UnregisterHandler(0);
            _pointerService.UnregisterUntouchableHandler();
            _constructionView?.Hide();
        }
    }
}