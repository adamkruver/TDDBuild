using Sources.Controllers.Credits;
using Sources.Domain.Credits;
using Sources.Infrastructure.Resource;
using Sources.Presentation.Ui;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Ui
{
    public class MoneyUiFactory
    {
        private readonly ResourceService _resourceService;

        public MoneyUiFactory(ResourceService resourceService) => 
            _resourceService = resourceService;

        public TextUi Create(Money money)
        {
            TextUi ui = Object.Instantiate(_resourceService.Load<TextUi>("Ui/Credits/MoneyUi"));
            MoneyPresenter presenter = new MoneyPresenter(ui, money);
            
            return ui;
        }
    }
}