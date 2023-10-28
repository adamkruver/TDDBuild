using Sources.Domain.Constructs;
using Sources.Infrastructure.Factories.Controllers.Constructs;
using Sources.Presentation.Views.Constructs;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Ui
{
    public class ConstructButtonUiFactory
    {
        private readonly ConstructButtonPresenterFactory _constructButtonPresenterFactory;
        private readonly string _viewPath = "Ui/Buttons/Constructs/ConstructButtonUi";

        public ConstructButtonUiFactory(ConstructButtonPresenterFactory constructButtonPresenterFactory)
        {
            _constructButtonPresenterFactory = constructButtonPresenterFactory;
        }

        public ConstructButtonUi Create(ConstructButton constructButton)
        {
            ConstructButtonUi ui = Object.Instantiate(Resources.Load<ConstructButtonUi>(_viewPath));
            var presenter = _constructButtonPresenterFactory.Create(ui, constructButton);
            
            ui.Construct(presenter);

            return ui;
        }
    }
}