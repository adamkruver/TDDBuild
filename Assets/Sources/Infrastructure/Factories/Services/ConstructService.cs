using System;
using Sources.Infrastructure.Handlers.Pointers;
using Sources.Infrastructure.Handlers.Pointers.Untouchable;
using Sources.Infrastructure.Services.Payments;
using Sources.Infrastructure.Services.Tilemaps;
using Sources.InfrastructureInterfaces.Services.Pointers;
using Sources.Presentation.Views.Cameras;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Services
{
    public class ConstructService
    {
        private readonly IPointerService _pointerService;
        private readonly GameplayCamera _gameplayCamera;
        private readonly TilemapUntouchablePointerHandler _tilemapUntouchablePointerHandler;
        private readonly PaymentService _paymentService;
        private readonly TilemapService _tilemapService;

        public ConstructService(
            IPointerService pointerService,
            PaymentService paymentService,
            TilemapService tilemapService,
            GameplayCamera gameplayCamera,
            TilemapUntouchablePointerHandler tilemapUntouchablePointerHandler
        )
        {
            _pointerService = pointerService;
            _paymentService = paymentService;
            _tilemapService = tilemapService;
            _gameplayCamera = gameplayCamera;
            _tilemapUntouchablePointerHandler = tilemapUntouchablePointerHandler;
        }

        public void Enable(Action<Vector2Int> onClick, int money)
        {
            Disable();

            _pointerService.RegisterHandler(
                0,
                new GameplayInteractPointerHandler(
                    _gameplayCamera, position =>
                    {
                        if(_paymentService.TryPay(money))
                            onClick.Invoke(position);
                        
                        Disable();
                    }
                )
            );
            
            _pointerService.RegisterUntouchableHandler(_tilemapUntouchablePointerHandler);
        }

        public void Disable()
        {
            _pointerService.UnregisterHandler(0);
            _pointerService.UnregisterUntouchableHandler();
            _tilemapService.HideTileInfo();
        }
    }
}