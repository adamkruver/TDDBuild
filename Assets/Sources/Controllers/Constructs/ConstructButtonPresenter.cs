using System;
using Sources.Domain.Constructs;
using Sources.Infrastructure.Factories.Services;
using Sources.Infrastructure.Services.Payments;
using Sources.PresentationInterfaces.Views.Constructions;
using Sources.PresentationInterfaces.Views.Constructs;
using UnityEngine;

namespace Sources.Controllers.Constructs
{
    public class ConstructButtonPresenter : PresenterBase
    {
        private readonly IConstructButtonUi _ui;
        private readonly ConstructButton _constructButton;
        private readonly PaymentService _paymentService;
        private readonly ConstructService _constructService;
        private readonly Func<IConstructionView>  _constructionViewFactory;

        public ConstructButtonPresenter(
            IConstructButtonUi ui,
            ConstructButton constructButton,
            PaymentService paymentService,
            ConstructService constructService,
            Func<IConstructionView> constructionViewFactory
        )
        {
            _ui = ui;
            _constructButton = constructButton;
            _paymentService = paymentService;
            _constructService = constructService;
            _constructionViewFactory = constructionViewFactory;
            
            _ui.SetPrice(_constructButton.Price.ToString());
            _ui.SetTitle(_constructButton.Title);
            _ui.SetIconSprite(_constructButton.IconSprite);
        }

        public override void Enable()
        {
            _paymentService.MoneyChanged += UpdateView;
            UpdateView();
            _ui.AddClickListener(OnButtonClick);        
        }

        public override void Disable()
        {
            _paymentService.MoneyChanged -= UpdateView;
            _ui.RemoveClickListener(OnButtonClick);        
        }

        private void UpdateView()
        {
            if (_paymentService.IsEnough(_constructButton.Price))
                _ui.Enable();
            else
                _ui.Disable();
        }

        private void OnButtonClick()
        {
            if (_paymentService.IsEnough(_constructButton.Price))
                _constructService.Enable(_constructionViewFactory.Invoke(), _constructButton.Price);
        }
    }
}