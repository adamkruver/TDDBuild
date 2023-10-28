using System;
using Sources.Domain.Constructs;
using Sources.Infrastructure.Listeners.Pointers;
using Sources.Infrastructure.Listeners.Pointers.Untouchable;
using Sources.InfrastructureInterfaces.Services.Pointers;
using Sources.PresentationInterfaces.Views.Constructs;

namespace Sources.Controllers.Constructs
{
    public class ConstructButtonPresenter
    {
        private readonly IConstructButtonUi _ui;
        private readonly ConstructButton _constructButton;
        private readonly IPointerService _pointerService;
        private readonly GameplayInteractPointerHandler _constructPointerHandler;
        private readonly TilemapUntouchablePointerHandler _constructUntouchablePointerHandler;

        private bool _isEnabled = false;

        public ConstructButtonPresenter(
            IConstructButtonUi ui,
            ConstructButton constructButton,
            IPointerService pointerService,
            GameplayInteractPointerHandler constructPointerHandler,
            TilemapUntouchablePointerHandler constructUntouchablePointerHandler
        )
        {
            _ui = ui;
            _constructButton = constructButton;
            _pointerService = pointerService;
            _constructPointerHandler = constructPointerHandler;
            _constructUntouchablePointerHandler = constructUntouchablePointerHandler;
            
            UpdateView();
        }

        public void Enable()
        {
            if (_isEnabled)
                return;

            _pointerService.RegisterHandler(0, _constructPointerHandler);
            _pointerService.RegisterUntouchableHandler(_constructUntouchablePointerHandler);

            _isEnabled = true;
        }

        public void Disable()
        {
            if (_isEnabled == false)
                return;

            _pointerService.UnregisterHandler(0);
            _pointerService.UnregisterUntouchableHandler();

            _isEnabled = false;
        }

        public void Build(Action callback)
        {
            Enable();
        }

        private void UpdateView()
        {
            _ui.SetPrice(_constructButton.Price.ToString());
            _ui.SetTitle(_constructButton.Title);
            _ui.SetIconSprite(_constructButton.IconSprite);
        }
    }
}